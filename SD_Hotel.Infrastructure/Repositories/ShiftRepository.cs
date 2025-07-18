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
    public class ShiftRepository : GenericRepository<Shift>, IShiftRepository
    {
        public ShiftRepository(SD_HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Shift>> GetByEmployeeAsync(int employeeId)
        {
            return await _context.Shifts
                .Where(s => s.EmployeeId == employeeId && s.IsActive)
                .OrderByDescending(s => s.ShiftDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shift>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Shifts
                .Where(s => s.ShiftDate >= startDate && s.ShiftDate <= endDate && s.IsActive)
                .OrderBy(s => s.ShiftDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shift>> GetByShiftTypeAsync(string shiftType)
        {
            return await _context.Shifts
                .Where(s => s.ShiftType.ToString() == shiftType && s.IsActive)
                .OrderByDescending(s => s.ShiftDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Shift>> GetActiveShiftsAsync()
        {
            return await _context.Shifts
                .Where(s => s.IsActive)
                .OrderByDescending(s => s.ShiftDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalHoursAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _context.Shifts
                .Where(s => s.EmployeeId == employeeId && 
                           s.ShiftDate >= startDate && 
                           s.ShiftDate <= endDate && 
                           s.IsActive)
                .SumAsync(s => s.HoursWorked);
        }
    }
} 