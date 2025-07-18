using SD_Hotel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Hotel.Core.Repositories
{
    public interface IShiftRepository : IGenericRepository<Shift>
    {
        Task<IEnumerable<Shift>> GetByEmployeeAsync(int employeeId);
        Task<IEnumerable<Shift>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Shift>> GetByShiftTypeAsync(string shiftType);
        Task<IEnumerable<Shift>> GetActiveShiftsAsync();
        Task<decimal> GetTotalHoursAsync(int employeeId, DateTime startDate, DateTime endDate);
    }
} 