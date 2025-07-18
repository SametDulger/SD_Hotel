using Microsoft.EntityFrameworkCore;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;
using SD_Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SD_Hotel.Core.Entities.ExtraExpense;

namespace SD_Hotel.Infrastructure.Repositories
{
    public class ExtraExpenseRepository : GenericRepository<ExtraExpense>, IExtraExpenseRepository
    {
        public ExtraExpenseRepository(SD_HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ExtraExpense>> GetByTypeAsync(string expenseType)
        {
            if (Enum.TryParse<ExpenseType>(expenseType, out var expenseTypeEnum))
            {
                return await _context.ExtraExpenses
                    .Where(e => e.ExpenseType == expenseTypeEnum && e.IsActive)
                    .ToListAsync();
            }
            return new List<ExtraExpense>();
        }

        public async Task<IEnumerable<ExtraExpense>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.ExtraExpenses
                .Where(e => e.ExpenseDate >= startDate && e.ExpenseDate <= endDate && e.IsActive)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalExpensesAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.ExtraExpenses.Where(e => e.IsActive);

            if (startDate.HasValue)
                query = query.Where(e => e.ExpenseDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(e => e.ExpenseDate <= endDate.Value);

            return await query.SumAsync(e => e.Amount);
        }
    }
} 