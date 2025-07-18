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
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public EmployeesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var employees = await _httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
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
                var employee = await _httpClient.GetFromJsonAsync<EmployeeDto>($"{_apiBaseUrl}/employees/{id}");
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
                    var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/employees", createEmployeeDto);
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
                var employee = await _httpClient.GetFromJsonAsync<EmployeeDto>($"{_apiBaseUrl}/employees/{id}");
                if (employee == null)
                    return NotFound();

                var updateDto = new UpdateEmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    IdentityNumber = employee.IdentityNumber,
                    DateOfBirth = employee.DateOfBirth,
                    HireDate = employee.HireDate,
                    Role = employee.Role,
                    Salary = employee.Salary,
                    Address = employee.Address,
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
                    var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/employees/{id}", updateEmployeeDto);
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
                var employee = await _httpClient.GetFromJsonAsync<EmployeeDto>($"{_apiBaseUrl}/employees/{id}");
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
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/employees/{id}");
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
                var employees = await _httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees/role/{role}");
                ViewBag.Role = role;
                ViewBag.Title = $"Rol: {role}";
                return View("Index", employees ?? new List<EmployeeDto>());
            }
            catch
            {
                ViewBag.Role = role;
                ViewBag.Title = $"Rol: {role}";
                return View("Index", new List<EmployeeDto>());
            }
        }

        public async Task<IActionResult> ByIdentityNumber(string identityNumber)
        {
            try
            {
                var employee = await _httpClient.GetFromJsonAsync<EmployeeDto>($"{_apiBaseUrl}/employees/identity/{identityNumber}");
                if (employee == null)
                {
                    ViewBag.Message = "Bu kimlik numarası ile personel bulunamadı.";
                    return View("Index", new List<EmployeeDto>());
                }
                return View("Details", employee);
            }
            catch
            {
                ViewBag.Message = "Bu kimlik numarası ile personel bulunamadı.";
                return View("Index", new List<EmployeeDto>());
            }
        }

        public async Task<IActionResult> ActiveEmployees()
        {
            try
            {
                var employees = await _httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees/active");
                ViewBag.Title = "Aktif Personel";
                return View("Index", employees ?? new List<EmployeeDto>());
            }
            catch
            {
                ViewBag.Title = "Aktif Personel";
                return View("Index", new List<EmployeeDto>());
            }
        }
    }
} 