using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Hotel.Application.DTOs;

namespace SD_Hotel.Application.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> CreateAsync(CreateCustomerDto createCustomerDto);
        Task<CustomerDto?> UpdateAsync(UpdateCustomerDto updateCustomerDto);
        Task DeleteAsync(int id);
        Task<CustomerDto?> GetByEmailAsync(string email);
        Task<CustomerDto?> GetByIdentityNumberAsync(string identityNumber);
        Task<IEnumerable<CustomerDto>> GetLoyaltyMembersAsync();
        Task<IEnumerable<CustomerDto>> SearchAsync(string searchTerm);
    }
} 