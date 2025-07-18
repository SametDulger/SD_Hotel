using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Hotel.Core.Entities;

namespace SD_Hotel.Core.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<Customer> GetCustomerByIdentityNumberAsync(string identityNumber);
        Task<IEnumerable<Customer>> GetLoyaltyMembersAsync();
        Task<Customer> GetCustomerWithReservationsAsync(int id);
        Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);
    }
} 