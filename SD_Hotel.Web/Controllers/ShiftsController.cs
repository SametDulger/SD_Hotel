using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SD_Hotel.Web.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public ShiftsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var shifts = await httpClient.GetFromJsonAsync<List<ShiftDto>>($"{_apiBaseUrl}/shifts");
                return View(shifts ?? new List<ShiftDto>());
            }
            catch
            {
                return View(new List<ShiftDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var shift = await httpClient.GetFromJsonAsync<ShiftDto>($"{_apiBaseUrl}/shifts/{id}");
                if (shift == null)
                    return NotFound();

                return View(shift);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var employees = await httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
                ViewBag.Employees = employees ?? new List<EmployeeDto>();
            }
            catch
            {
                ViewBag.Employees = new List<EmployeeDto>();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateShiftDto createShiftDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PostAsJsonAsync($"{_apiBaseUrl}/shifts", createShiftDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Vardiya oluşturulurken bir hata oluştu.");
                }
            }

            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var employees = await httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
                ViewBag.Employees = employees ?? new List<EmployeeDto>();
            }
            catch
            {
                ViewBag.Employees = new List<EmployeeDto>();
            }

            return View(createShiftDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var shift = await httpClient.GetFromJsonAsync<ShiftDto>($"{_apiBaseUrl}/shifts/{id}");
                if (shift == null)
                    return NotFound();

                var employees = await httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
                ViewBag.Employees = employees ?? new List<EmployeeDto>();

                var updateDto = new UpdateShiftDto
                {
                    Id = shift.Id,
                    EmployeeId = shift.EmployeeId,
                    EmployeeName = shift.EmployeeName,
                    ShiftDate = shift.ShiftDate,
                    StartTime = shift.StartTime,
                    EndTime = shift.EndTime,
                    ShiftType = shift.ShiftType,
                    IsCompleted = shift.IsCompleted,
                    Notes = shift.Notes
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
        public async Task<IActionResult> Edit(int id, UpdateShiftDto updateShiftDto)
        {
            if (id != updateShiftDto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PutAsJsonAsync($"{_apiBaseUrl}/shifts/{id}", updateShiftDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Vardiya güncellenirken bir hata oluştu.");
                }
            }

            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var employees = await httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
                ViewBag.Employees = employees ?? new List<EmployeeDto>();
            }
            catch
            {
                ViewBag.Employees = new List<EmployeeDto>();
            }

            return View(updateShiftDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var shift = await httpClient.GetFromJsonAsync<ShiftDto>($"{_apiBaseUrl}/shifts/{id}");
                if (shift == null)
                    return NotFound();

                return View(shift);
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
                var response = await httpClient.DeleteAsync($"{_apiBaseUrl}/shifts/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Vardiya silinirken bir hata oluştu.");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ByEmployee(int employeeId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var shifts = await httpClient.GetFromJsonAsync<List<ShiftDto>>($"{_apiBaseUrl}/shifts/employee/{employeeId}");
                ViewBag.EmployeeId = employeeId;
                return View("Index", shifts ?? new List<ShiftDto>());
            }
            catch
            {
                ViewBag.EmployeeId = employeeId;
                return View("Index", new List<ShiftDto>());
            }
        }

        public async Task<IActionResult> ByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var shifts = await httpClient.GetFromJsonAsync<List<ShiftDto>>($"{_apiBaseUrl}/shifts/date-range?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                return View("Index", shifts ?? new List<ShiftDto>());
            }
            catch
            {
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                return View("Index", new List<ShiftDto>());
            }
        }

        public async Task<IActionResult> ByShiftType(string shiftType)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var shifts = await httpClient.GetFromJsonAsync<List<ShiftDto>>($"{_apiBaseUrl}/shifts/type/{shiftType}");
                ViewBag.ShiftType = shiftType;
                return View("Index", shifts ?? new List<ShiftDto>());
            }
            catch
            {
                ViewBag.ShiftType = shiftType;
                return View("Index", new List<ShiftDto>());
            }
        }

        public async Task<IActionResult> ActiveShifts()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var shifts = await httpClient.GetFromJsonAsync<List<ShiftDto>>($"{_apiBaseUrl}/shifts/active");
                ViewBag.Title = "Aktif Vardiyalar";
                return View("Index", shifts ?? new List<ShiftDto>());
            }
            catch
            {
                ViewBag.Title = "Aktif Vardiyalar";
                return View("Index", new List<ShiftDto>());
            }
        }

        public async Task<IActionResult> TotalHours(int employeeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var totalHours = await httpClient.GetFromJsonAsync<decimal>($"{_apiBaseUrl}/shifts/total-hours?employeeId={employeeId}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                ViewBag.EmployeeId = employeeId;
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.TotalHours = totalHours;
                return View();
            }
            catch
            {
                ViewBag.EmployeeId = employeeId;
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.TotalHours = 0;
                return View();
            }
        }
    }
} 