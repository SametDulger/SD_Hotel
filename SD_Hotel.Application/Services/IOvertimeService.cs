using SD_Hotel.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Hotel.Application.Services
{
    public interface IOvertimeService
    {
        Task<IEnumerable<OvertimeDto>> GetAllAsync();
        Task<OvertimeDto?> GetByIdAsync(int id);
        Task<OvertimeDto?> CreateAsync(CreateOvertimeDto createOvertimeDto);
        Task<OvertimeDto?> UpdateAsync(UpdateOvertimeDto updateOvertimeDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<OvertimeDto>> GetByEmployeeAsync(int employeeId);
        Task<IEnumerable<OvertimeDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<OvertimeDto>> GetPendingApprovalsAsync();
        Task<decimal> GetTotalOvertimeHoursAsync(int employeeId, DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalOvertimePayAsync(int employeeId, DateTime startDate, DateTime endDate);
    }
} 