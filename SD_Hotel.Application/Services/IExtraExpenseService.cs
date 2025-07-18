using SD_Hotel.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Hotel.Application.Services
{
    public interface IExtraExpenseService
    {
        Task<IEnumerable<ExtraExpenseDto>> GetAllAsync();
        Task<ExtraExpenseDto> GetByIdAsync(int id);
        Task<ExtraExpenseDto> CreateAsync(CreateExtraExpenseDto createExtraExpenseDto);
        Task<ExtraExpenseDto> UpdateAsync(UpdateExtraExpenseDto updateExtraExpenseDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ExtraExpenseDto>> GetByTypeAsync(string expenseType);
        Task<IEnumerable<ExtraExpenseDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalExpensesAsync(DateTime? startDate = null, DateTime? endDate = null);
    }
} 