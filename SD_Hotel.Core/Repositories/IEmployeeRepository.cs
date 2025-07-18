using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Hotel.Core.Entities;

namespace SD_Hotel.Core.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(EmployeeRole role);
        Task<Employee> GetEmployeeByIdentityNumberAsync(string identityNumber);
        Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
        Task<Employee> GetEmployeeWithShiftsAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesByShiftTypeAsync(ShiftType shiftType, DateTime date);
    }
} 