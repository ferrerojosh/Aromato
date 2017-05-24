using System;
using System.Collections.Generic;
using Aromato.Application;
using Aromato.Application.Web.Data;
using AspNet.Security.OpenIdConnect.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aromato.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [Authorize("employee.read")]
        [HttpGet]
        public IEnumerable<IData> Index()
        {
            return _employeeService.RetrieveAll();
        }

        [Authorize("employee.read")]
        [HttpGet("{id}")]
        public IData Get(long id)
        {
            return _employeeService.RetrieveById(id);
        }

        [Authorize("employee.write")]
        [HttpPost]
        public void Create([FromBody] EmployeeWebData data)
        {
            _employeeService.CreateEmployee(data);
        }

        [Authorize]
        [HttpPut("{id}/email")]
        public void ChangeEmail(long id, [FromBody] EmployeeWebData data)
        {
            if (CanModifySelf(id) || User.HasClaim("scope", "employee.modify"))
            {
                _employeeService.ChangeEmail(id, data.Email);
            }
            else
            {
                HttpContext.Response.StatusCode = 403;
            }
        }

        private bool CanModifySelf(long employeeId)
        {
            if (User.HasClaim("scope", "employee.self"))
            {
                return long.Parse(User.GetClaim("id")) == employeeId;
            }
            return false;
        }

        [Authorize]
        [HttpPut("{id}/contact_no")]
        public void ChangeContactNo(long id, [FromBody] EmployeeWebData data)
        {
            if (CanModifySelf(id) || User.HasClaim("scope", "employee.modify"))
            {
                _employeeService.ChangeContactNo(id, data.ContactNo);
            }
            else
            {
                HttpContext.Response.StatusCode = 403;
            }
        }

        [Authorize("employee.delete")]
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _employeeService.DeleteEmployee(id);
        }

        [HttpPut("{uniqueId}/punch")]
        public void Punch(string uniqueId)
        {
            _employeeService.Punch(uniqueId);
        }
    }
}