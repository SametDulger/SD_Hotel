using Microsoft.EntityFrameworkCore;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;
using SD_Hotel.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SD_Hotel.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SD_HotelDbContext context) : base(context)
        {
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Email == email && c.IsActive);
        }

        public async Task<Customer> GetCustomerByIdentityNumberAsync(string identityNumber)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.IdentityNumber == identityNumber && c.IsActive);
        }

        public async Task<IEnumerable<Customer>> GetLoyaltyMembersAsync()
        {
            return await _dbSet.Where(c => c.IsLoyaltyMember && c.IsActive)
                              .OrderByDescending(c => c.LoyaltyPoints)
                              .ToListAsync();
        }

        public async Task<Customer> GetCustomerWithReservationsAsync(int id)
        {
            return await _dbSet.Where(c => c.Id == id && c.IsActive)
                              .Include(c => c.Reservations)
                              .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
        {
            return await _dbSet.Where(c => c.IsActive &&
                                          (c.FirstName.Contains(searchTerm) ||
                                           c.LastName.Contains(searchTerm) ||
                                           c.Email.Contains(searchTerm) ||
                                           c.Phone.Contains(searchTerm) ||
                                           c.IdentityNumber.Contains(searchTerm)))
                              .OrderBy(c => c.FirstName)
                              .ThenBy(c => c.LastName)
                              .ToListAsync();
        }
    }
} 