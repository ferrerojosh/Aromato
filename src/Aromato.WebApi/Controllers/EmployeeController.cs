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

        // GET: api/Employee
        [HttpGet]
        public IEnumerable<IData<long, Employee>> Get()
        {
            return _employeeService.RetrieveAll();
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public IData<long, Employee> Get(long id)
        {
            return _employeeService.RetrieveById(id);
        }
        
        // POST: api/Employee
        [HttpPost]
        public void Post([FromBody] EmployeeWebData data)
        {
            Console.WriteLine(data);
            _employeeService.CreateEmployee(data);
        }
        
        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
