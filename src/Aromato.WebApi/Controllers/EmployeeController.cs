using System;
using System.Collections.Generic;
using Aromato.Application;
using Aromato.Application.Web.Data;
using Aromato.Domain.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/employee
        [HttpGet]
        public IEnumerable<IData> GetEmployees()
        {
            return _employeeService.RetrieveAll();
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public IData GetEmployee(long id)
        {
            return _employeeService.RetrieveById(id);
        }
        
        // POST: api/employee
        [HttpPost]
        public dynamic CreateEmployee([FromBody] EmployeeWebData data)
        {
            return DoActionOrFail(() => _employeeService.CreateEmployee(data));

        }
        // PUT: api/employee/5/email
        [HttpPut("{id}/email")]
        public dynamic ChangeEmail(long id, [FromBody] EmployeeWebData data)
        {
            return DoActionOrFail(() => _employeeService.ChangeEmail(id, data.Email));

            //_employeeService.ChangeEmail(id, data.Email);
        }

        [HttpPut("{id}/punch")]
        public dynamic Punch(long id)
        {
            try
            {
                return new RestResponse
                {
                    Success = true,
                    Data = _employeeService.Punch(id)
                };
            }
            catch (Exception e)
            {
                return new RestResponse
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        // PUT: api/employee/5/contactno
        [HttpPut("{id}/contactno")]
        public dynamic ChangeContactNo(long id, [FromBody] EmployeeWebData data)
        {
            return DoActionOrFail(() => _employeeService.ChangeContactNo(id, data.ContactNo));
        }

        private RestResponse DoActionOrFail(Action action)
        {
            var response = new RestResponse();
            try
            {
                action.Invoke();
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
