using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SD_Hotel.Application.DTOs;
using SD_Hotel.Core.Repositories;

namespace SD_Hotel.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                IdentityNumber = customer.IdentityNumber,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                IsLoyaltyMember = customer.IsLoyaltyMember,
                LoyaltyPoints = customer.LoyaltyPoints
            };
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                IdentityNumber = c.IdentityNumber,
                Address = c.Address,
                City = c.City,
                Country = c.Country,
                IsLoyaltyMember = c.IsLoyaltyMember,
                LoyaltyPoints = c.LoyaltyPoints
            });
        }

        public async Task<CustomerDto?> CreateAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = new Core.Entities.Customer
            {
                FirstName = createCustomerDto.FirstName,
                LastName = createCustomerDto.LastName,
                Email = createCustomerDto.Email,
                Phone = createCustomerDto.Phone,
                IdentityNumber = createCustomerDto.IdentityNumber,
                Address = createCustomerDto.Address,
                City = createCustomerDto.City,
                Country = createCustomerDto.Country
            };

            var createdCustomer = await _customerRepository.AddAsync(customer);
            return await GetByIdAsync(createdCustomer.Id);
        }

        public async Task<CustomerDto?> UpdateAsync(UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _customerRepository.GetByIdAsync(updateCustomerDto.Id);
            if (customer == null) return null;

            customer.FirstName = updateCustomerDto.FirstName;
            customer.LastName = updateCustomerDto.LastName;
            customer.Email = updateCustomerDto.Email;
            customer.Phone = updateCustomerDto.Phone;
            customer.IdentityNumber = updateCustomerDto.IdentityNumber;
            customer.Address = updateCustomerDto.Address;
            customer.City = updateCustomerDto.City;
            customer.Country = updateCustomerDto.Country;
            customer.IsLoyaltyMember = updateCustomerDto.IsLoyaltyMember;
            customer.LoyaltyPoints = updateCustomerDto.LoyaltyPoints;

            await _customerRepository.UpdateAsync(customer);
            return await GetByIdAsync(customer.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        public async Task<CustomerDto?> GetByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(email);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                IdentityNumber = customer.IdentityNumber,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                IsLoyaltyMember = customer.IsLoyaltyMember,
                LoyaltyPoints = customer.LoyaltyPoints
            };
        }

        public async Task<CustomerDto?> GetByIdentityNumberAsync(string identityNumber)
        {
            var customer = await _customerRepository.GetCustomerByIdentityNumberAsync(identityNumber);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                IdentityNumber = customer.IdentityNumber,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                IsLoyaltyMember = customer.IsLoyaltyMember,
                LoyaltyPoints = customer.LoyaltyPoints
            };
        }

        public async Task<IEnumerable<CustomerDto>> GetLoyaltyMembersAsync()
        {
            var customers = await _customerRepository.GetLoyaltyMembersAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                IdentityNumber = c.IdentityNumber,
                Address = c.Address,
                City = c.City,
                Country = c.Country,
                IsLoyaltyMember = c.IsLoyaltyMember,
                LoyaltyPoints = c.LoyaltyPoints
            });
        }

        public async Task<IEnumerable<CustomerDto>> SearchAsync(string searchTerm)
        {
            var customers = await _customerRepository.SearchCustomersAsync(searchTerm);
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                IdentityNumber = c.IdentityNumber,
                Address = c.Address,
                City = c.City,
                Country = c.Country,
                IsLoyaltyMember = c.IsLoyaltyMember,
                LoyaltyPoints = c.LoyaltyPoints
            });
        }
    }
} 