using SD_Hotel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Hotel.Core.Repositories
{
    public interface IOvertimeRepository : IGenericRepository<Overtime>
    {
        Task<IEnumerable<Overtime>> GetByEmployeeAsync(int employeeId);
        Task<IEnumerable<Overtime>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Overtime>> GetPendingApprovalsAsync();
        Task<decimal> GetTotalOvertimeHoursAsync(int employeeId, DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalOvertimePayAsync(int employeeId, DateTime startDate, DateTime endDate);
    }
} 