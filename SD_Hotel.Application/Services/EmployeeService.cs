using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SD_Hotel.Application.DTOs;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;

namespace SD_Hotel.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                IdentityNumber = employee.IdentityNumber,
                Email = employee.Email,
                Phone = employee.Phone,
                Address = employee.Address,
                Role = employee.Role.ToString(),
                HourlyRate = employee.HourlyRate,
                MonthlySalary = employee.MonthlySalary,
                HireDate = employee.HireDate,
                IsActive = employee.IsActive
            };
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                IdentityNumber = e.IdentityNumber,
                Email = e.Email,
                Phone = e.Phone,
                Address = e.Address,
                Role = e.Role.ToString(),
                HourlyRate = e.HourlyRate,
                MonthlySalary = e.MonthlySalary,
                HireDate = e.HireDate,
                IsActive = e.IsActive
            });
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employee = new Employee
            {
                FirstName = createEmployeeDto.FirstName,
                LastName = createEmployeeDto.LastName,
                IdentityNumber = createEmployeeDto.IdentityNumber,
                Email = createEmployeeDto.Email,
                Phone = createEmployeeDto.Phone,
                Address = createEmployeeDto.Address,
                Role = Enum.Parse<EmployeeRole>(createEmployeeDto.Role),
                HourlyRate = createEmployeeDto.HourlyRate,
                MonthlySalary = createEmployeeDto.MonthlySalary,
                HireDate = createEmployeeDto.HireDate
            };

            var createdEmployee = await _employeeRepository.AddAsync(employee);
            return await GetByIdAsync(createdEmployee.Id);
        }

        public async Task<EmployeeDto> UpdateAsync(UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(updateEmployeeDto.Id);
            if (employee == null) return null;

            employee.FirstName = updateEmployeeDto.FirstName;
            employee.LastName = updateEmployeeDto.LastName;
            employee.IdentityNumber = updateEmployeeDto.IdentityNumber;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Address = updateEmployeeDto.Address;
            employee.Role = Enum.Parse<EmployeeRole>(updateEmployeeDto.Role);
            employee.HourlyRate = updateEmployeeDto.HourlyRate;
            employee.MonthlySalary = updateEmployeeDto.MonthlySalary;
            employee.HireDate = updateEmployeeDto.HireDate;
            employee.IsActive = updateEmployeeDto.IsActive;

            await _employeeRepository.UpdateAsync(employee);
            return await GetByIdAsync(employee.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EmployeeDto>> GetByRoleAsync(string role)
        {
            var roleEnum = Enum.Parse<EmployeeRole>(role);
            var employees = await _employeeRepository.GetEmployeesByRoleAsync(roleEnum);
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                IdentityNumber = e.IdentityNumber,
                Email = e.Email,
                Phone = e.Phone,
                Address = e.Address,
                Role = e.Role.ToString(),
                HourlyRate = e.HourlyRate,
                MonthlySalary = e.MonthlySalary,
                HireDate = e.HireDate,
                IsActive = e.IsActive
            });
        }

        public async Task<EmployeeDto> GetByIdentityNumberAsync(string identityNumber)
        {
            var employee = await _employeeRepository.GetEmployeeByIdentityNumberAsync(identityNumber);
            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                IdentityNumber = employee.IdentityNumber,
                Email = employee.Email,
                Phone = employee.Phone,
                Address = employee.Address,
                Role = employee.Role.ToString(),
                HourlyRate = employee.HourlyRate,
                MonthlySalary = employee.MonthlySalary,
                HireDate = employee.HireDate,
                IsActive = employee.IsActive
            };
        }

        public async Task<IEnumerable<EmployeeDto>> GetActiveEmployeesAsync()
        {
            var employees = await _employeeRepository.GetActiveEmployeesAsync();
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                IdentityNumber = e.IdentityNumber,
                Email = e.Email,
                Phone = e.Phone,
                Address = e.Address,
                Role = e.Role.ToString(),
                HourlyRate = e.HourlyRate,
                MonthlySalary = e.MonthlySalary,
                HireDate = e.HireDate,
                IsActive = e.IsActive
            });
        }
    }
} 