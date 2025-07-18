using Microsoft.EntityFrameworkCore;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;
using SD_Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SD_Hotel.Infrastructure.Repositories
{
    public class OvertimeRepository : GenericRepository<Overtime>, IOvertimeRepository
    {
        public OvertimeRepository(SD_HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Overtime>> GetByEmployeeAsync(int employeeId)
        {
            return await _context.Overtimes
                .Where(o => o.EmployeeId == employeeId && o.IsActive)
                .OrderByDescending(o => o.OvertimeDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Overtime>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Overtimes
                .Where(o => o.OvertimeDate >= startDate && o.OvertimeDate <= endDate && o.IsActive)
                .OrderBy(o => o.OvertimeDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Overtime>> GetPendingApprovalsAsync()
        {
            return await _context.Overtimes
                .Where(o => !o.IsApproved && o.IsActive)
                .OrderByDescending(o => o.OvertimeDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalOvertimeHoursAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _context.Overtimes
                .Where(o => o.EmployeeId == employeeId && 
                           o.OvertimeDate >= startDate && 
                           o.OvertimeDate <= endDate && 
                           o.IsActive)
                .SumAsync(o => o.HoursWorked);
        }

        public async Task<decimal> GetTotalOvertimePayAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _context.Overtimes
                .Where(o => o.EmployeeId == employeeId && 
                           o.OvertimeDate >= startDate && 
                           o.OvertimeDate <= endDate && 
                           o.IsActive)
                .SumAsync(o => o.TotalPay);
        }
    }
} 