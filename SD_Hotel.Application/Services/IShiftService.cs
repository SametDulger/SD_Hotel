using SD_Hotel.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Hotel.Application.Services
{
    public interface IShiftService
    {
        Task<IEnumerable<ShiftDto>> GetAllAsync();
        Task<ShiftDto> GetByIdAsync(int id);
        Task<ShiftDto> CreateAsync(CreateShiftDto createShiftDto);
        Task<ShiftDto> UpdateAsync(UpdateShiftDto updateShiftDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ShiftDto>> GetByEmployeeAsync(int employeeId);
        Task<IEnumerable<ShiftDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<ShiftDto>> GetByShiftTypeAsync(string shiftType);
        Task<IEnumerable<ShiftDto>> GetActiveShiftsAsync();
        Task<decimal> GetTotalHoursAsync(int employeeId, DateTime startDate, DateTime endDate);
    }
} 