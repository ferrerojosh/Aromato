using System.Collections.Generic;
using Aromato.Application;
using Aromato.Application.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize("employee.read")]
        public IEnumerable<IData> Index()
        {
            return _employeeService.RetrieveAll();
        }

        [HttpGet("{id}")]
        [Authorize("employee.read")]
        public IData Get(long id)
        {
            return _employeeService.RetrieveById(id);
        }

        [HttpPost]
        [Authorize("employee.write")]
        public void Create([FromBody] EmployeeWebData data)
        {
            _employeeService.CreateEmployee(data);
        }

        [Authorize("employee.write")]
        [HttpPut("{id}/email")]
        public void ChangeEmail(long id, [FromBody] EmployeeWebData data)
        {
            _employeeService.ChangeEmail(id, data.Email);
        }

        [Authorize("employee.write")]
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