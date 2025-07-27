using SD_Hotel.Application.DTOs;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SD_Hotel.Core.Entities.Shift;

namespace SD_Hotel.Application.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ShiftService(IShiftRepository shiftRepository, IEmployeeRepository employeeRepository)
        {
            _shiftRepository = shiftRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<ShiftDto>> GetAllAsync()
        {
            var shifts = await _shiftRepository.GetAllAsync();
            var shiftDtos = new List<ShiftDto>();
            
            foreach (var shift in shifts)
            {
                shiftDtos.Add(await MapToDto(shift));
            }
            
            return shiftDtos;
        }

        public async Task<ShiftDto?> GetByIdAsync(int id)
        {
            var shift = await _shiftRepository.GetByIdAsync(id);
            return shift != null ? await MapToDto(shift) : null;
        }

        public async Task<ShiftDto?> CreateAsync(CreateShiftDto createShiftDto)
        {
            if (Enum.TryParse<ShiftType>(createShiftDto.ShiftType, out var shiftTypeEnum))
            {
                var hoursWorked = (decimal)(createShiftDto.EndTime - createShiftDto.StartTime).TotalHours;

                var shift = new Shift
                {
                    EmployeeId = createShiftDto.EmployeeId,
                    ShiftDate = createShiftDto.ShiftDate,
                    StartTime = createShiftDto.StartTime,
                    EndTime = createShiftDto.EndTime,
                    HoursWorked = hoursWorked,
                    ShiftType = shiftTypeEnum,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };

                await _shiftRepository.AddAsync(shift);
                return await MapToDto(shift);
            }
            throw new ArgumentException("Invalid shift type");
        }

        public async Task<ShiftDto?> UpdateAsync(UpdateShiftDto updateShiftDto)
        {
            var shift = await _shiftRepository.GetByIdAsync(updateShiftDto.Id);
            if (shift == null)
                return null;

            if (Enum.TryParse<ShiftType>(updateShiftDto.ShiftType, out var shiftTypeEnum))
            {
                var hoursWorked = (decimal)(updateShiftDto.EndTime - updateShiftDto.StartTime).TotalHours;

                shift.EmployeeId = updateShiftDto.EmployeeId;
                shift.ShiftDate = updateShiftDto.ShiftDate;
                shift.StartTime = updateShiftDto.StartTime;
                shift.EndTime = updateShiftDto.EndTime;
                shift.HoursWorked = hoursWorked;
                shift.ShiftType = shiftTypeEnum;
                shift.IsActive = updateShiftDto.IsActive;
                shift.UpdatedDate = DateTime.UtcNow;

                await _shiftRepository.UpdateAsync(shift);
                return await MapToDto(shift);
            }
            throw new ArgumentException("Invalid shift type");
        }

        public async Task DeleteAsync(int id)
        {
            await _shiftRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ShiftDto>> GetByEmployeeAsync(int employeeId)
        {
            var shifts = await _shiftRepository.GetByEmployeeAsync(employeeId);
            var shiftDtos = new List<ShiftDto>();
            
            foreach (var shift in shifts)
            {
                shiftDtos.Add(await MapToDto(shift));
            }
            
            return shiftDtos;
        }

        public async Task<IEnumerable<ShiftDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var shifts = await _shiftRepository.GetByDateRangeAsync(startDate, endDate);
            var shiftDtos = new List<ShiftDto>();
            
            foreach (var shift in shifts)
            {
                shiftDtos.Add(await MapToDto(shift));
            }
            
            return shiftDtos;
        }

        public async Task<IEnumerable<ShiftDto>> GetByShiftTypeAsync(string shiftType)
        {
            var shifts = await _shiftRepository.GetByShiftTypeAsync(shiftType);
            var shiftDtos = new List<ShiftDto>();
            
            foreach (var shift in shifts)
            {
                shiftDtos.Add(await MapToDto(shift));
            }
            
            return shiftDtos;
        }

        public async Task<IEnumerable<ShiftDto>> GetActiveShiftsAsync()
        {
            var shifts = await _shiftRepository.GetActiveShiftsAsync();
            var shiftDtos = new List<ShiftDto>();
            
            foreach (var shift in shifts)
            {
                shiftDtos.Add(await MapToDto(shift));
            }
            
            return shiftDtos;
        }

        public async Task<decimal> GetTotalHoursAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _shiftRepository.GetTotalHoursAsync(employeeId, startDate, endDate);
        }

        private async Task<ShiftDto> MapToDto(Shift shift)
        {
            var employee = await _employeeRepository.GetByIdAsync(shift.EmployeeId);
            return new ShiftDto
            {
                Id = shift.Id,
                EmployeeId = shift.EmployeeId,
                EmployeeName = employee != null ? $"{employee.FirstName} {employee.LastName}" : "Unknown",
                ShiftDate = shift.ShiftDate,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                HoursWorked = shift.HoursWorked,
                ShiftType = shift.ShiftType.ToString(),
                IsActive = shift.IsActive,
                CreatedDate = shift.CreatedDate,
                UpdatedDate = shift.UpdatedDate
            };
        }
    }
} 