using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using SD_Hotel.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(CreateCustomerDto createCustomerDto)
        {
            var customer = await _customerService.CreateAsync(createCustomerDto);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> Update(int id, UpdateCustomerDto updateCustomerDto)
        {
            if (id != updateCustomerDto.Id)
                return BadRequest();

            var customer = await _customerService.UpdateAsync(updateCustomerDto);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _customerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<CustomerDto>> GetByEmail(string email)
        {
            var customer = await _customerService.GetByEmailAsync(email);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("identity/{identityNumber}")]
        public async Task<ActionResult<CustomerDto>> GetByIdentityNumber(string identityNumber)
        {
            var customer = await _customerService.GetByIdentityNumberAsync(identityNumber);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("loyalty")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetLoyaltyMembers()
        {
            var customers = await _customerService.GetLoyaltyMembersAsync();
            return Ok(customers);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Search([FromQuery] string searchTerm)
        {
            var customers = await _customerService.SearchAsync(searchTerm);
            return Ok(customers);
        }
    }
} 