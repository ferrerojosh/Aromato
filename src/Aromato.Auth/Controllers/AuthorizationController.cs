using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aromato.Infrastructure.PostgreSQL;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Core;

namespace Aromato.Auth.Controllers
{
    public class AuthorizationController : ControllerBase
    {
        private readonly PostgresUnitOfWork _unitOfWork;

        public AuthorizationController(PostgresUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("~/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                var credentials = await _unitOfWork.Credentials
                    .Include("Employee.Roles.Role.Permissions.Permission")
                    .FirstOrDefaultAsync(c => c.Username == request.Username);

                if (credentials == null)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "Username/password is invalid."
                    });
                }

                var requestedRole = (string) request.GetParameter("role");

                if (string.IsNullOrEmpty(requestedRole))
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidRequest,
                        ErrorDescription = "Please specify role to invoke."
                    });

                if (!BCrypt.Net.BCrypt.Verify(request.Password, credentials.Password))
                    return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);

                // Create a new ClaimsIdentity holding the user identity.
                var identity = new ClaimsIdentity(
                    OpenIdConnectServerDefaults.AuthenticationScheme,
                    OpenIdConnectConstants.Claims.Name,
                    OpenIdConnectConstants.Claims.Role
                );
                // Add a "sub" claim containing the user identifier, and attach
                // the "access_token" destination to allow OpenIddict to store it
                // in the access token, so it can be retrieved from your controllers.

                identity.AddClaim(OpenIdConnectConstants.Claims.Subject, credentials.Employee.UniqueId);
                identity.AddClaim(OpenIdConnectConstants.Claims.Name, credentials.Employee.Name);

                var empRole = credentials.Employee.Roles.FirstOrDefault(r => r.Role.Name == requestedRole);

                if (empRole == null)
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "User does not have that role."
                    });

                var role = empRole.Role;

                identity.AddClaim(OpenIdConnectConstants.Claims.Role, role.Name);

                foreach (var rolePerm in role.Permissions)
                {
                    var perm = rolePerm.Permission;
                    identity.AddClaim(OpenIdConnectConstants.Claims.Scope, perm.Name);
                }

                // Add roles and permissions
//            foreach(var empRole in credentials.Employee.Roles)
//            {
//                var role = empRole.Role;
//                identity.AddClaim(OpenIdConnectConstants.Claims.Role, role.Name);
//
//                foreach(var rolePerm in role.Permissions)
//                {
//                    var perm = rolePerm.Permission;
//                    identity.AddClaim(OpenIdConnectConstants.Claims.Scope, perm.Name);
//                }
//            }

                // ... add other claims, if necessary.
                var principal = new ClaimsPrincipal(identity);

                // Claims will not be associated with specific destinations by default, so we must indicate whether they should
                // be included or not in access and identity tokens.
                foreach (var claim in principal.Claims)
                {
                    // For this sample, just include all claims in all token types.
                    // In reality, claims' destinations would probably differ by token type and depending on the scopes requested.
                    claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken,
                        OpenIdConnectConstants.Destinations.IdentityToken);
                }

                // Create a new authentication ticket for the user's principal
                var ticket = new AuthenticationTicket(
                    principal,
                    new AuthenticationProperties(),
                    OpenIdConnectServerDefaults.AuthenticationScheme);

                // Include resources and scopes, as appropriate
                var scope = new[]
                {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Email,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess,
                    OpenIddictConstants.Scopes.Roles
                }.Intersect(request.GetScopes());

                // Set resource servers
                ticket.SetResources("aromato");
                ticket.SetScopes(scope);

                // Sign in the user
                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }
            // TODO add refresh token grant type
            else
            {
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidRequest,
                    ErrorDescription = "The specified grant type is not supported."
                });
            }
        }
    }
}