using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.Auth.Controllers
{

    public class UserInfoController : ControllerBase
    {
        [HttpGet("~/api/userinfo"), Produces("application/json")]
        [Authorize]
        public UserInfo Index()
        {
            return new UserInfo
            {
                Subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject),
                FamilyName = User.GetClaim(OpenIdConnectConstants.Claims.FamilyName),
                Gender = User.GetClaim(OpenIdConnectConstants.Claims.Gender),
                Email = User.GetClaim(OpenIdConnectConstants.Claims.Email),
                GivenName = User.GetClaim(OpenIdConnectConstants.Claims.GivenName),
                Name = User.GetClaim(OpenIdConnectConstants.Claims.Name),
                PhoneNumber = User.GetClaim(OpenIdConnectConstants.Claims.PhoneNumber),
                Username = User.GetClaim(OpenIdConnectConstants.Claims.Username)
            };
        }
    }
}