using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SD_Hotel.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public EmployeesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var employees = await httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
                return View(employees ?? new List<EmployeeDto>());
            }
            catch
            {
                return View(new List<EmployeeDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var employee = await httpClient.GetFromJsonAsync<EmployeeDto>($"{_apiBaseUrl}/employees/{id}");
                if (employee == null)
                    return NotFound();

                return View(employee);
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
        public async Task<IActionResult> Create(CreateEmployeeDto createEmployeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PostAsJsonAsync($"{_apiBaseUrl}/employees", createEmployeeDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Personel oluşturulurken bir hata oluştu.");
                }
            }
            return View(createEmployeeDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var employee = await httpClient.GetFromJsonAsync<EmployeeDto>($"{_apiBaseUrl}/employees/{id}");
                if (employee == null)
                    return NotFound();

                var updateDto = new UpdateEmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    IdentityNumber = employee.IdentityNumber,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Address = employee.Address,
                    Role = employee.Role,
                    HourlyRate = employee.HourlyRate,
                    MonthlySalary = employee.MonthlySalary,
                    HireDate = employee.HireDate,
                    IsActive = employee.IsActive
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
        public async Task<IActionResult> Edit(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (id != updateEmployeeDto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PutAsJsonAsync($"{_apiBaseUrl}/employees/{id}", updateEmployeeDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Personel güncellenirken bir hata oluştu.");
                }
            }
            return View(updateEmployeeDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var employee = await httpClient.GetFromJsonAsync<EmployeeDto>($"{_apiBaseUrl}/employees/{id}");
                if (employee == null)
                    return NotFound();

                return View(employee);
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
                var response = await httpClient.DeleteAsync($"{_apiBaseUrl}/employees/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Personel silinirken bir hata oluştu.");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ByRole(string role)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var employees = await httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees/role/{role}");
                ViewBag.Role = role;
                return View("Index", employees ?? new List<EmployeeDto>());
            }
            catch
            {
                ViewBag.Role = role;
                return View("Index", new List<EmployeeDto>());
            }
        }

        public async Task<IActionResult> ByIdentityNumber(string identityNumber)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var employee = await httpClient.GetFromJsonAsync<EmployeeDto>($"{_apiBaseUrl}/employees/identity/{identityNumber}");
                if (employee == null)
                    return NotFound();

                return View("Details", employee);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> ActiveEmployees()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var employees = await httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees/active");
                return View("Index", employees ?? new List<EmployeeDto>());
            }
            catch
            {
                return View("Index", new List<EmployeeDto>());
            }
        }
    }
} 