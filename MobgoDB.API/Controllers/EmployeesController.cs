using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobgoDB.API.Models;
using MobgoDB.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobgoDB.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private EmployeeService _employeeService;
        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            List<Employee> employees = _employeeService.Get();
            return Ok(employees);
        }

        [HttpGet("{Id}")]
        public ActionResult<Employee> Get(string Id)
        {
            var emp = _employeeService.Get(Id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

    }
}
