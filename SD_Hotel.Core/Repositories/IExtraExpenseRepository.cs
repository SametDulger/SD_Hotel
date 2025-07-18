using SD_Hotel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Hotel.Core.Repositories
{
    public interface IExtraExpenseRepository : IGenericRepository<ExtraExpense>
    {
        Task<IEnumerable<ExtraExpense>> GetByTypeAsync(string expenseType);
        Task<IEnumerable<ExtraExpense>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalExpensesAsync(DateTime? startDate = null, DateTime? endDate = null);
    }
} 