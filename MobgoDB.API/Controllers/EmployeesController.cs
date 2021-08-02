using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobgoDB.API.Models;
using MobgoDB.API.Services;
using System;
using System.Collections.Generic;

namespace MobgoDB.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private EmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(EmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService ?? throw new ArgumentException(nameof(EmployeeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            List<Employee> employees = _employeeService.Get();
            return Ok(employees);
        }

        [HttpGet("{Id}", Name = "GetEmployee")]
        public ActionResult<Employee> Get(string Id)
        {
            var emp = _employeeService.Get(Id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        [HttpPost()]
        public ActionResult<Employee> Add([FromBody] EmployeeForCreationDto employeeForCreation)
        {
            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);
            _employeeService.Add(employeeEntity);
            return CreatedAtRoute("GetEmployee", new { Id = employeeEntity.Id }, employeeEntity);
        }

        [HttpPut()]
        public ActionResult<Employee> Update([FromBody] Employee employee)
        {
            _employeeService.Update(employee);
            return CreatedAtRoute("GetEmployee", new { Id = employee.Id }, employee);
        }

        [HttpDelete("{Id}")]
        public ActionResult<List<Employee>> Delete(string Id)
        {
            _employeeService.Delete(Id);
            return NoContent();
        }

    }
}
