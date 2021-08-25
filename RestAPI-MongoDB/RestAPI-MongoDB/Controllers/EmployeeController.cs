using Microsoft.AspNetCore.Mvc;
using RestAPI_MongoDB.Models;
using RestAPI_MongoDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get() =>
            _employeeService.Get();

        [HttpGet("{id:length(24)}", Name = "Nantharupan")]
        public ActionResult<Employee> Get(string id)
        {
            var employee = _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public ActionResult<Employee> Create(Employee employee)
        {
            _employeeService.Create(employee);

            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Employee employeeIn)
        {
            var book = _employeeService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _employeeService.Update(id, employeeIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _employeeService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _employeeService.Remove(book.Id);

            return NoContent();
        }
    }
}