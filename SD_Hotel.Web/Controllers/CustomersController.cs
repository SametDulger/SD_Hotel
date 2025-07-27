using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SD_Hotel.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var customers = await httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers");
                return View(customers ?? new List<CustomerDto>());
            }
            catch
            {
                return View(new List<CustomerDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var customer = await httpClient.GetFromJsonAsync<CustomerDto>($"{_apiBaseUrl}/customers/{id}");
                if (customer == null)
                    return NotFound();

                return View(customer);
            }
            catch
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerDto createCustomerDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PostAsJsonAsync($"{_apiBaseUrl}/customers", createCustomerDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Müşteri oluşturulurken bir hata oluştu.");
                }
            }
            return View(createCustomerDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var customer = await httpClient.GetFromJsonAsync<CustomerDto>($"{_apiBaseUrl}/customers/{id}");
                if (customer == null)
                    return NotFound();

                var updateDto = new UpdateCustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    IdentityNumber = customer.IdentityNumber,
                    DateOfBirth = customer.DateOfBirth,
                    Address = customer.Address,
                    IsLoyaltyMember = customer.IsLoyaltyMember
                };

                return View(updateDto);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCustomerDto updateCustomerDto)
        {
            if (id != updateCustomerDto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PutAsJsonAsync($"{_apiBaseUrl}/customers/{id}", updateCustomerDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Müşteri güncellenirken bir hata oluştu.");
                }
            }
            return View(updateCustomerDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var customer = await httpClient.GetFromJsonAsync<CustomerDto>($"{_apiBaseUrl}/customers/{id}");
                if (customer == null)
                    return NotFound();

                return View(customer);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var response = await httpClient.DeleteAsync($"{_apiBaseUrl}/customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Müşteri silinirken bir hata oluştu.");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ByEmail(string email)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var customer = await httpClient.GetFromJsonAsync<CustomerDto>($"{_apiBaseUrl}/customers/email/{email}");
                if (customer == null)
                    return NotFound();

                return View("Details", customer);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> ByIdentityNumber(string identityNumber)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var customer = await httpClient.GetFromJsonAsync<CustomerDto>($"{_apiBaseUrl}/customers/identity/{identityNumber}");
                if (customer == null)
                    return NotFound();

                return View("Details", customer);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> LoyaltyMembers()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var customers = await httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers/loyalty");
                return View(customers ?? new List<CustomerDto>());
            }
            catch
            {
                return View(new List<CustomerDto>());
            }
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var customers = await httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers/search?searchTerm={searchTerm}");
                ViewBag.SearchTerm = searchTerm;
                return View("Index", customers ?? new List<CustomerDto>());
            }
            catch
            {
                ViewBag.SearchTerm = searchTerm;
                return View("Index", new List<CustomerDto>());
            }
        }
    }
} 