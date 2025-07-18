using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Hotel.Application.DTOs;

namespace SD_Hotel.Application.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> CreateAsync(CreateEmployeeDto createEmployeeDto);
        Task<EmployeeDto> UpdateAsync(UpdateEmployeeDto updateEmployeeDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetByRoleAsync(string role);
        Task<EmployeeDto> GetByIdentityNumberAsync(string identityNumber);
        Task<IEnumerable<EmployeeDto>> GetActiveEmployeesAsync();
    }
} 