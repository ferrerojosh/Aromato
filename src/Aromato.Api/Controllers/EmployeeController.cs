using System.Collections.Generic;
using Aromato.Application;
using Aromato.Application.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.Api.Controllers
{
    [Authorize(Roles = "sysad")]
    [Produces("application/json")]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IEnumerable<IData> Index()
        {
            return _employeeService.RetrieveAll();
        }

        [HttpGet("{id}")]
        public IData Get(long id)
        {
            return _employeeService.RetrieveById(id);
        }

        [HttpPost]
        public void Create([FromBody] EmployeeWebData data)
        {
            _employeeService.CreateEmployee(data);
        }

        [HttpPut("{id}/email")]
        public void ChangeEmail(long id, [FromBody] EmployeeWebData data)
        {
            _employeeService.ChangeEmail(id, data.Email);
        }

        [HttpPut("{id}/contact_no")]
        public void ChangeContactNo(long id, [FromBody] EmployeeWebData data)
        {
            _employeeService.ChangeContactNo(id, data.ContactNo);
        }

        [HttpPut("{uniqueId}/punch")]
        public void Punch(string uniqueId)
        {
            _employeeService.Punch(uniqueId);
        }
    }
}