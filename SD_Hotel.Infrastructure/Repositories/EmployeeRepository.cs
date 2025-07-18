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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(SD_HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(EmployeeRole role)
        {
            return await _dbSet.Where(e => e.Role == role && e.IsActive)
                              .OrderBy(e => e.FirstName)
                              .ThenBy(e => e.LastName)
                              .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdentityNumberAsync(string identityNumber)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.IdentityNumber == identityNumber && e.IsActive);
        }

        public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
        {
            return await _dbSet.Where(e => e.IsActive)
                              .OrderBy(e => e.FirstName)
                              .ThenBy(e => e.LastName)
                              .ToListAsync();
        }

        public async Task<Employee> GetEmployeeWithShiftsAsync(int id)
        {
            return await _dbSet.Where(e => e.Id == id && e.IsActive)
                              .Include(e => e.Shifts)
                              .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByShiftTypeAsync(ShiftType shiftType, DateTime date)
        {
            return await _dbSet.Where(e => e.IsActive)
                              .Include(e => e.Shifts.Where(s => s.ShiftType == shiftType && s.ShiftDate.Date == date.Date))
                              .Where(e => e.Shifts.Any(s => s.ShiftType == shiftType && s.ShiftDate.Date == date.Date))
                              .OrderBy(e => e.FirstName)
                              .ThenBy(e => e.LastName)
                              .ToListAsync();
        }
    }
} 