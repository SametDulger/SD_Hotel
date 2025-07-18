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
    public class ExtraExpensesController : ControllerBase
    {
        private readonly IExtraExpenseService _extraExpenseService;

        public ExtraExpensesController(IExtraExpenseService extraExpenseService)
        {
            _extraExpenseService = extraExpenseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtraExpenseDto>>> GetAll()
        {
            var expenses = await _extraExpenseService.GetAllAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExtraExpenseDto>> GetById(int id)
        {
            var expense = await _extraExpenseService.GetByIdAsync(id);
            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult<ExtraExpenseDto>> Create(CreateExtraExpenseDto createExtraExpenseDto)
        {
            var expense = await _extraExpenseService.CreateAsync(createExtraExpenseDto);
            return CreatedAtAction(nameof(GetById), new { id = expense.Id }, expense);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExtraExpenseDto>> Update(int id, UpdateExtraExpenseDto updateExtraExpenseDto)
        {
            if (id != updateExtraExpenseDto.Id)
                return BadRequest();

            var expense = await _extraExpenseService.UpdateAsync(updateExtraExpenseDto);
            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _extraExpenseService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("type/{expenseType}")]
        public async Task<ActionResult<IEnumerable<ExtraExpenseDto>>> GetByType(string expenseType)
        {
            var expenses = await _extraExpenseService.GetByTypeAsync(expenseType);
            return Ok(expenses);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<ExtraExpenseDto>>> GetByDateRange(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            var expenses = await _extraExpenseService.GetByDateRangeAsync(startDate, endDate);
            return Ok(expenses);
        }

        [HttpGet("total")]
        public async Task<ActionResult<decimal>> GetTotalExpenses(
            [FromQuery] DateTime? startDate = null, 
            [FromQuery] DateTime? endDate = null)
        {
            var total = await _extraExpenseService.GetTotalExpensesAsync(startDate, endDate);
            return Ok(total);
        }
    }
} 