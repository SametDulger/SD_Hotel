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
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftsController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetAll()
        {
            var shifts = await _shiftService.GetAllAsync();
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDto>> GetById(int id)
        {
            var shift = await _shiftService.GetByIdAsync(id);
            if (shift == null)
                return NotFound();

            return Ok(shift);
        }

        [HttpPost]
        public async Task<ActionResult<ShiftDto>> Create(CreateShiftDto createShiftDto)
        {
            var shift = await _shiftService.CreateAsync(createShiftDto);
            return CreatedAtAction(nameof(GetById), new { id = shift.Id }, shift);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShiftDto>> Update(int id, UpdateShiftDto updateShiftDto)
        {
            if (id != updateShiftDto.Id)
                return BadRequest();

            var shift = await _shiftService.UpdateAsync(updateShiftDto);
            if (shift == null)
                return NotFound();

            return Ok(shift);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _shiftService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetByEmployee(int employeeId)
        {
            var shifts = await _shiftService.GetByEmployeeAsync(employeeId);
            return Ok(shifts);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetByDateRange(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            var shifts = await _shiftService.GetByDateRangeAsync(startDate, endDate);
            return Ok(shifts);
        }

        [HttpGet("type/{shiftType}")]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetByShiftType(string shiftType)
        {
            var shifts = await _shiftService.GetByShiftTypeAsync(shiftType);
            return Ok(shifts);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetActiveShifts()
        {
            var shifts = await _shiftService.GetActiveShiftsAsync();
            return Ok(shifts);
        }

        [HttpGet("total-hours")]
        public async Task<ActionResult<decimal>> GetTotalHours(
            [FromQuery] int employeeId, 
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            var totalHours = await _shiftService.GetTotalHoursAsync(employeeId, startDate, endDate);
            return Ok(totalHours);
        }
    }
} 