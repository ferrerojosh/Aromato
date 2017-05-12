using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetMessage()
        {
            return new JsonResult(new
            {
                Subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject),
                Name = User.Identity.Name
            });
        }
    }
}