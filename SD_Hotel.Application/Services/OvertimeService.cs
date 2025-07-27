using SD_Hotel.Application.DTOs;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SD_Hotel.Application.Services
{
    public class OvertimeService : IOvertimeService
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public OvertimeService(IOvertimeRepository overtimeRepository, IEmployeeRepository employeeRepository)
        {
            _overtimeRepository = overtimeRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<OvertimeDto>> GetAllAsync()
        {
            var overtimes = await _overtimeRepository.GetAllAsync();
            var overtimeDtos = new List<OvertimeDto>();
            
            foreach (var overtime in overtimes)
            {
                overtimeDtos.Add(await MapToDto(overtime));
            }
            
            return overtimeDtos;
        }

        public async Task<OvertimeDto?> GetByIdAsync(int id)
        {
            var overtime = await _overtimeRepository.GetByIdAsync(id);
            return overtime != null ? await MapToDto(overtime) : null;
        }

        public async Task<OvertimeDto?> CreateAsync(CreateOvertimeDto createOvertimeDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(createOvertimeDto.EmployeeId);
            if (employee == null)
                throw new ArgumentException("Employee not found");

            var hoursWorked = (decimal)(createOvertimeDto.EndTime - createOvertimeDto.StartTime).TotalHours;
            var totalPay = hoursWorked * employee.HourlyRate * 1.5m; // 1.5x overtime rate

            var overtime = new Overtime
            {
                EmployeeId = createOvertimeDto.EmployeeId,
                OvertimeDate = createOvertimeDto.OvertimeDate,
                StartTime = createOvertimeDto.StartTime,
                EndTime = createOvertimeDto.EndTime,
                HoursWorked = hoursWorked,
                HourlyRate = employee.HourlyRate,
                TotalPay = totalPay,
                Reason = createOvertimeDto.Reason,
                IsApproved = false,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            await _overtimeRepository.AddAsync(overtime);
            return await MapToDto(overtime);
        }

        public async Task<OvertimeDto?> UpdateAsync(UpdateOvertimeDto updateOvertimeDto)
        {
            var overtime = await _overtimeRepository.GetByIdAsync(updateOvertimeDto.Id);
            if (overtime == null)
                return null;

            var hoursWorked = (decimal)(updateOvertimeDto.EndTime - updateOvertimeDto.StartTime).TotalHours;
            var totalPay = hoursWorked * overtime.HourlyRate * 1.5m;

            overtime.EmployeeId = updateOvertimeDto.EmployeeId;
            overtime.OvertimeDate = updateOvertimeDto.OvertimeDate;
            overtime.StartTime = updateOvertimeDto.StartTime;
            overtime.EndTime = updateOvertimeDto.EndTime;
            overtime.HoursWorked = hoursWorked;
            overtime.TotalPay = totalPay;
            overtime.Reason = updateOvertimeDto.Reason;
            overtime.IsApproved = updateOvertimeDto.IsApproved;
            overtime.IsActive = updateOvertimeDto.IsActive;
            overtime.UpdatedDate = DateTime.UtcNow;

            await _overtimeRepository.UpdateAsync(overtime);
            return await MapToDto(overtime);
        }

        public async Task DeleteAsync(int id)
        {
            await _overtimeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OvertimeDto>> GetByEmployeeAsync(int employeeId)
        {
            var overtimes = await _overtimeRepository.GetByEmployeeAsync(employeeId);
            var overtimeDtos = new List<OvertimeDto>();
            
            foreach (var overtime in overtimes)
            {
                overtimeDtos.Add(await MapToDto(overtime));
            }
            
            return overtimeDtos;
        }

        public async Task<IEnumerable<OvertimeDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var overtimes = await _overtimeRepository.GetByDateRangeAsync(startDate, endDate);
            var overtimeDtos = new List<OvertimeDto>();
            
            foreach (var overtime in overtimes)
            {
                overtimeDtos.Add(await MapToDto(overtime));
            }
            
            return overtimeDtos;
        }

        public async Task<IEnumerable<OvertimeDto>> GetPendingApprovalsAsync()
        {
            var overtimes = await _overtimeRepository.GetPendingApprovalsAsync();
            var overtimeDtos = new List<OvertimeDto>();
            
            foreach (var overtime in overtimes)
            {
                overtimeDtos.Add(await MapToDto(overtime));
            }
            
            return overtimeDtos;
        }

        public async Task<decimal> GetTotalOvertimeHoursAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _overtimeRepository.GetTotalOvertimeHoursAsync(employeeId, startDate, endDate);
        }

        public async Task<decimal> GetTotalOvertimePayAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _overtimeRepository.GetTotalOvertimePayAsync(employeeId, startDate, endDate);
        }

        private async Task<OvertimeDto> MapToDto(Overtime overtime)
        {
            var employee = await _employeeRepository.GetByIdAsync(overtime.EmployeeId);
            return new OvertimeDto
            {
                Id = overtime.Id,
                EmployeeId = overtime.EmployeeId,
                EmployeeName = employee != null ? $"{employee.FirstName} {employee.LastName}" : "Unknown",
                OvertimeDate = overtime.OvertimeDate,
                StartTime = overtime.StartTime,
                EndTime = overtime.EndTime,
                HoursWorked = overtime.HoursWorked,
                HourlyRate = overtime.HourlyRate,
                TotalPay = overtime.TotalPay,
                Reason = overtime.Reason,
                IsApproved = overtime.IsApproved,
                IsActive = overtime.IsActive,
                CreatedDate = overtime.CreatedDate,
                UpdatedDate = overtime.UpdatedDate
            };
        }
    }
} 