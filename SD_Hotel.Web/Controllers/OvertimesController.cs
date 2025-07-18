using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SD_Hotel.Web.Controllers
{
    public class OvertimesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public OvertimesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var overtimes = await _httpClient.GetFromJsonAsync<List<OvertimeDto>>($"{_apiBaseUrl}/overtimes");
                return View(overtimes ?? new List<OvertimeDto>());
            }
            catch
            {
                return View(new List<OvertimeDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var overtime = await _httpClient.GetFromJsonAsync<OvertimeDto>($"{_apiBaseUrl}/overtimes/{id}");
                if (overtime == null)
                    return NotFound();

                return View(overtime);
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
                var employees = await _httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
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
        public async Task<IActionResult> Create(CreateOvertimeDto createOvertimeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/overtimes", createOvertimeDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Mesai oluşturulurken bir hata oluştu.");
                }
            }
            
            try
            {
                var employees = await _httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
                ViewBag.Employees = employees ?? new List<EmployeeDto>();
            }
            catch
            {
                ViewBag.Employees = new List<EmployeeDto>();
            }
            
            return View(createOvertimeDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var overtime = await _httpClient.GetFromJsonAsync<OvertimeDto>($"{_apiBaseUrl}/overtimes/{id}");
                if (overtime == null)
                    return NotFound();

                var employees = await _httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");

                var updateDto = new UpdateOvertimeDto
                {
                    Id = overtime.Id,
                    EmployeeId = overtime.EmployeeId,
                    OvertimeDate = overtime.OvertimeDate,
                    StartTime = overtime.StartTime,
                    EndTime = overtime.EndTime,
                    Reason = overtime.Reason,
                    IsApproved = overtime.IsApproved,
                    IsActive = overtime.IsActive
                };

                ViewBag.Employees = employees ?? new List<EmployeeDto>();

                return View(updateDto);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateOvertimeDto updateOvertimeDto)
        {
            if (id != updateOvertimeDto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/overtimes/{id}", updateOvertimeDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Mesai güncellenirken bir hata oluştu.");
                }
            }
            
            try
            {
                var employees = await _httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
                ViewBag.Employees = employees ?? new List<EmployeeDto>();
            }
            catch
            {
                ViewBag.Employees = new List<EmployeeDto>();
            }
            
            return View(updateOvertimeDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var overtime = await _httpClient.GetFromJsonAsync<OvertimeDto>($"{_apiBaseUrl}/overtimes/{id}");
                if (overtime == null)
                    return NotFound();

                return View(overtime);
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
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/overtimes/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Mesai silinirken bir hata oluştu.");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ByEmployee(int employeeId)
        {
            try
            {
                var overtimes = await _httpClient.GetFromJsonAsync<List<OvertimeDto>>($"{_apiBaseUrl}/overtimes/employee/{employeeId}");
                ViewBag.EmployeeId = employeeId;
                ViewBag.Title = $"Personel ID: {employeeId} Mesaileri";
                return View("Index", overtimes ?? new List<OvertimeDto>());
            }
            catch
            {
                ViewBag.EmployeeId = employeeId;
                ViewBag.Title = $"Personel ID: {employeeId} Mesaileri";
                return View("Index", new List<OvertimeDto>());
            }
        }

        public async Task<IActionResult> ByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var overtimes = await _httpClient.GetFromJsonAsync<List<OvertimeDto>>($"{_apiBaseUrl}/overtimes/date-range?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.Title = $"Tarih Aralığı: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}";
                return View("Index", overtimes ?? new List<OvertimeDto>());
            }
            catch
            {
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.Title = $"Tarih Aralığı: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}";
                return View("Index", new List<OvertimeDto>());
            }
        }

        public async Task<IActionResult> PendingApprovals()
        {
            try
            {
                var overtimes = await _httpClient.GetFromJsonAsync<List<OvertimeDto>>($"{_apiBaseUrl}/overtimes/pending-approvals");
                ViewBag.Title = "Onay Bekleyen Mesailer";
                return View("Index", overtimes ?? new List<OvertimeDto>());
            }
            catch
            {
                ViewBag.Title = "Onay Bekleyen Mesailer";
                return View("Index", new List<OvertimeDto>());
            }
        }

        public async Task<IActionResult> TotalHours(int employeeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var totalHours = await _httpClient.GetFromJsonAsync<decimal>($"{_apiBaseUrl}/overtimes/total-hours?employeeId={employeeId}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
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

        public async Task<IActionResult> TotalPay(int employeeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var totalPay = await _httpClient.GetFromJsonAsync<decimal>($"{_apiBaseUrl}/overtimes/total-pay?employeeId={employeeId}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                ViewBag.EmployeeId = employeeId;
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.TotalPay = totalPay;
                return View();
            }
            catch
            {
                ViewBag.EmployeeId = employeeId;
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.TotalPay = 0;
                return View();
            }
        }
    }
} 