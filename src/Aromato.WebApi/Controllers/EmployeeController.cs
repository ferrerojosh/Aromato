using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aromato.Application;
using Aromato.Application.Web.Data;
using Aromato.Domain.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService<long, Employee> _employeeService;

        public EmployeeController(IEmployeeService<long, Employee> employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/employee
        [HttpGet]
        public IEnumerable<IData<long, Employee>> Get()
        {
            return _employeeService.RetrieveAll();
        }

        // GET: api/employee/5
        [HttpGet("{id}", Name = "Get")]
        public IData<long, Employee> Get(long id)
        {
            return _employeeService.RetrieveById(id);
        }
        
        // POST: api/employee
        [HttpPost]
        public dynamic Post([FromBody] EmployeeWebData data)
        {
            return DoActionOrFail(() => _employeeService.CreateEmployee(data));

        }
        // PUT: api/employee/5/email
        [HttpPut("{id}/email")]
        public dynamic ChangeEmail(long id, [FromBody] EmployeeWebData data)
        {
            return DoActionOrFail(() => _employeeService.ChangeEmail(id, data.Email));
        }

        // PUT: api/employee/5/contactno
        [HttpPut("{id}/contactno")]
        public dynamic ChangeContactNo(long id, [FromBody] EmployeeWebData data)
        {
            return DoActionOrFail(() => _employeeService.ChangeContactNo(id, data.Email));
        }

        private RestResponse DoActionOrFail(Action action)
        {
            var response = new RestResponse();
            try
            {
                action.Invoke();
                response.Success = true;
            }
            catch (ArgumentException e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
