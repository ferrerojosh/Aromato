using System.Collections.Generic;
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

        [HttpGet("~/connect/logout")]
        public async Task<IActionResult> Logout()
        {
            // Returning a SignOutResult will ask OpenIddict to redirect the user agent
            // to the post_logout_redirect_uri specified by the client application.
            return SignOut(OpenIdConnectServerDefaults.AuthenticationScheme);
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
                    return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
                }

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

                identity.AddClaim(OpenIdConnectConstants.Claims.Username, credentials.Username,
                    OpenIdConnectConstants.Destinations.IdentityToken,
                    OpenIdConnectConstants.Destinations.AccessToken);
                identity.AddClaim(OpenIdConnectConstants.Claims.Subject, credentials.Employee.UniqueId, OpenIdConnectConstants.Destinations.IdentityToken);
                identity.AddClaim(OpenIdConnectConstants.Claims.Name, credentials.Employee.Name, OpenIdConnectConstants.Destinations.IdentityToken);

                // ... add other claims, if necessary.
                var principal = new ClaimsPrincipal(identity);

                // Claims will not be associated with specific destinations by default, so we must indicate whether they should
                // be included or not in access and identity tokens.
//                foreach (var claim in principal.Claims)
//                {
//                    // For this sample, just include all claims in all token types.
//                    // In reality, claims' destinations would probably differ by token type and depending on the scopes requested.
//                    var destinations = new List<string>
//                    {
//                        OpenIdConnectConstants.Destinations.AccessToken
//                    };
//
//                    // Only add the iterated claim to the id_token if the corresponding scope was granted to the client application.
//                    // The other claims will only be added to the access_token, which is encrypted when using the default format.
//                    if (claim.Type == OpenIdConnectConstants.Claims.Name ||
//                        claim.Type == OpenIdConnectConstants.Claims.Role)
//                    {
//                        destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
//                    }
//
//                    claim.SetDestinations(destinations);
//                }

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true
                };

                // Create a new authentication ticket for the user's principal
                var ticket = new AuthenticationTicket(
                    principal,
                    authProperties,
                    OpenIdConnectServerDefaults.AuthenticationScheme);

                var roles = credentials.Employee.Roles.Select(r => r.Role);
                var permissions = roles.SelectMany(p => p.Permissions,
                    (role, permission) => permission.Permission.Name).ToList();

                // common scopes
                permissions.AddRange(new[]
                {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Email,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess,
                    OpenIddictConstants.Scopes.Roles
                });

                // Include resources and scopes, as appropriate
                var scope = permissions.Intersect(request.GetScopes());

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