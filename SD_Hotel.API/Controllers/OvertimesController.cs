using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using SD_Hotel.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OvertimesController : ControllerBase
    {
        private readonly IOvertimeService _overtimeService;

        public OvertimesController(IOvertimeService overtimeService)
        {
            _overtimeService = overtimeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OvertimeDto>>> GetAll()
        {
            var overtimes = await _overtimeService.GetAllAsync();
            return Ok(overtimes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OvertimeDto>> GetById(int id)
        {
            var overtime = await _overtimeService.GetByIdAsync(id);
            if (overtime == null)
                return NotFound();

            return Ok(overtime);
        }

        [HttpPost]
        public async Task<ActionResult<OvertimeDto>> Create(CreateOvertimeDto createOvertimeDto)
        {
            var overtime = await _overtimeService.CreateAsync(createOvertimeDto);
            if (overtime == null)
                return BadRequest();
            return CreatedAtAction(nameof(GetById), new { id = overtime.Id }, overtime);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OvertimeDto>> Update(int id, UpdateOvertimeDto updateOvertimeDto)
        {
            if (id != updateOvertimeDto.Id)
                return BadRequest();

            var overtime = await _overtimeService.UpdateAsync(updateOvertimeDto);
            if (overtime == null)
                return NotFound();

            return Ok(overtime);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _overtimeService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<OvertimeDto>>> GetByEmployee(int employeeId)
        {
            var overtimes = await _overtimeService.GetByEmployeeAsync(employeeId);
            return Ok(overtimes);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<OvertimeDto>>> GetByDateRange(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            var overtimes = await _overtimeService.GetByDateRangeAsync(startDate, endDate);
            return Ok(overtimes);
        }

        [HttpGet("pending-approvals")]
        public async Task<ActionResult<IEnumerable<OvertimeDto>>> GetPendingApprovals()
        {
            var overtimes = await _overtimeService.GetPendingApprovalsAsync();
            return Ok(overtimes);
        }

        [HttpGet("total-hours")]
        public async Task<ActionResult<decimal>> GetTotalOvertimeHours(
            [FromQuery] int employeeId, 
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            var totalHours = await _overtimeService.GetTotalOvertimeHoursAsync(employeeId, startDate, endDate);
            return Ok(totalHours);
        }

        [HttpGet("total-pay")]
        public async Task<ActionResult<decimal>> GetTotalOvertimePay(
            [FromQuery] int employeeId, 
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            var totalPay = await _overtimeService.GetTotalOvertimePayAsync(employeeId, startDate, endDate);
            return Ok(totalPay);
        }
    }
} 