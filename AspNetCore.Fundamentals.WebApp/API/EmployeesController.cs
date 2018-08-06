using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Fundamentals.Domain.Dto;
using AspNetCore.Fundamentals.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Fundamentals.WebApp
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(EmployeeService employeeService
            , ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<(List<EmployeeDto> Employees, int TotalItems)>> Get()
        {
            var result = await _employeeService.GetEmployees("", "", 0, 100);
            return result;
        }
                
        [Route("CurrentUser")]
        public string GetUserName()
        {
            return User.Identity.Name;
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<EmployeeDto>> Get(Guid id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            return employee;
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Post([FromBody, Bind()] EmployeeDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _employeeService.AddEmployee(model);
            if (result)
                return CreatedAtAction(nameof(Get), new { id = model.Id }, model);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] EmployeeDto model)
        {
            model.Id = id;
            var result = await _employeeService.UpdateEmployee(model);
            if (result)
                return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            var result = await _employeeService.DeleteEmployee(employee);
            if (result)
                return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
