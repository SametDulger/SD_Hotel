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
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public CustomersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var customers = await _httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers");
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
                var customer = await _httpClient.GetFromJsonAsync<CustomerDto>($"{_apiBaseUrl}/customers/{id}");
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
                    var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/customers", createCustomerDto);
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
                var customer = await _httpClient.GetFromJsonAsync<CustomerDto>($"{_apiBaseUrl}/customers/{id}");
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
                    var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/customers/{id}", updateCustomerDto);
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
                var customer = await _httpClient.GetFromJsonAsync<CustomerDto>($"{_apiBaseUrl}/customers/{id}");
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
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/customers/{id}");
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
                var customer = await _httpClient.GetFromJsonAsync<CustomerDto>($"{_apiBaseUrl}/customers/email/{email}");
                if (customer == null)
                {
                    ViewBag.Message = "Bu e-posta adresi ile müşteri bulunamadı.";
                    return View("Index", new List<CustomerDto>());
                }
                return View("Details", customer);
            }
            catch
            {
                ViewBag.Message = "Bu e-posta adresi ile müşteri bulunamadı.";
                return View("Index", new List<CustomerDto>());
            }
        }

        public async Task<IActionResult> ByIdentityNumber(string identityNumber)
        {
            try
            {
                var customer = await _httpClient.GetFromJsonAsync<CustomerDto>($"{_apiBaseUrl}/customers/identity/{identityNumber}");
                if (customer == null)
                {
                    ViewBag.Message = "Bu kimlik numarası ile müşteri bulunamadı.";
                    return View("Index", new List<CustomerDto>());
                }
                return View("Details", customer);
            }
            catch
            {
                ViewBag.Message = "Bu kimlik numarası ile müşteri bulunamadı.";
                return View("Index", new List<CustomerDto>());
            }
        }

        public async Task<IActionResult> LoyaltyMembers()
        {
            try
            {
                var customers = await _httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers/loyalty");
                ViewBag.Title = "Sadakat Üyeleri";
                return View("Index", customers ?? new List<CustomerDto>());
            }
            catch
            {
                ViewBag.Title = "Sadakat Üyeleri";
                return View("Index", new List<CustomerDto>());
            }
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            try
            {
                var customers = await _httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers/search?searchTerm={searchTerm}");
                ViewBag.SearchTerm = searchTerm;
                ViewBag.Title = $"Arama Sonuçları: {searchTerm}";
                return View("Index", customers ?? new List<CustomerDto>());
            }
            catch
            {
                ViewBag.SearchTerm = searchTerm;
                ViewBag.Title = $"Arama Sonuçları: {searchTerm}";
                return View("Index", new List<CustomerDto>());
            }
        }
    }
} 