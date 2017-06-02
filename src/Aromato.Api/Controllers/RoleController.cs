using System.Collections.Generic;
using Aromato.Application;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize("roles")]
        [HttpGet]
        public IEnumerable<IData> Index()
        {
            var subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject);
            return _roleService.RetrieveRolesByUniqueId(subject);
        }
        
        [HttpGet("{username}")]
        public IEnumerable<IData> FindByName(string username)
        {
            return _roleService.RetrieveRolesByUsername(username);
        }
    }
}