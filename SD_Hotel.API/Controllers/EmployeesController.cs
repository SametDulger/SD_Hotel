using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using SD_Hotel.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(CreateEmployeeDto createEmployeeDto)
        {
            var employee = await _employeeService.CreateAsync(createEmployeeDto);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> Update(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (id != updateEmployeeDto.Id)
                return BadRequest();

            var employee = await _employeeService.UpdateAsync(updateEmployeeDto);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("role/{role}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetByRole(string role)
        {
            var employees = await _employeeService.GetByRoleAsync(role);
            return Ok(employees);
        }

        [HttpGet("identity/{identityNumber}")]
        public async Task<ActionResult<EmployeeDto>> GetByIdentityNumber(string identityNumber)
        {
            var employee = await _employeeService.GetByIdentityNumberAsync(identityNumber);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetActiveEmployees()
        {
            var employees = await _employeeService.GetActiveEmployeesAsync();
            return Ok(employees);
        }
    }
} 