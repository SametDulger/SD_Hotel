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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public OvertimesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var overtimes = await httpClient.GetFromJsonAsync<List<OvertimeDto>>($"{_apiBaseUrl}/overtimes");
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
                var httpClient = _httpClientFactory.CreateClient("API");
                var overtime = await httpClient.GetFromJsonAsync<OvertimeDto>($"{_apiBaseUrl}/overtimes/{id}");
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
        public async Task<IActionResult> Create(CreateOvertimeDto createOvertimeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PostAsJsonAsync($"{_apiBaseUrl}/overtimes", createOvertimeDto);
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
                var httpClient = _httpClientFactory.CreateClient("API");
                var employees = await httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
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
                var httpClient = _httpClientFactory.CreateClient("API");
                var overtime = await httpClient.GetFromJsonAsync<OvertimeDto>($"{_apiBaseUrl}/overtimes/{id}");
                if (overtime == null)
                    return NotFound();

                var employees = await httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
                ViewBag.Employees = employees ?? new List<EmployeeDto>();

                var updateDto = new UpdateOvertimeDto
                {
                    Id = overtime.Id,
                    EmployeeId = overtime.EmployeeId,
                    EmployeeName = overtime.EmployeeName,
                    OvertimeDate = overtime.OvertimeDate,
                    StartTime = overtime.StartTime,
                    EndTime = overtime.EndTime,
                    HourlyRate = overtime.HourlyRate,
                    TotalPay = overtime.TotalPay,
                    Reason = overtime.Reason,
                    IsApproved = overtime.IsApproved
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
        public async Task<IActionResult> Edit(int id, UpdateOvertimeDto updateOvertimeDto)
        {
            if (id != updateOvertimeDto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PutAsJsonAsync($"{_apiBaseUrl}/overtimes/{id}", updateOvertimeDto);
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
                var httpClient = _httpClientFactory.CreateClient("API");
                var employees = await httpClient.GetFromJsonAsync<List<EmployeeDto>>($"{_apiBaseUrl}/employees");
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
                var httpClient = _httpClientFactory.CreateClient("API");
                var overtime = await httpClient.GetFromJsonAsync<OvertimeDto>($"{_apiBaseUrl}/overtimes/{id}");
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
                var httpClient = _httpClientFactory.CreateClient("API");
                var response = await httpClient.DeleteAsync($"{_apiBaseUrl}/overtimes/{id}");
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
                var httpClient = _httpClientFactory.CreateClient("API");
                var overtimes = await httpClient.GetFromJsonAsync<List<OvertimeDto>>($"{_apiBaseUrl}/overtimes/employee/{employeeId}");
                ViewBag.EmployeeId = employeeId;
                return View("Index", overtimes ?? new List<OvertimeDto>());
            }
            catch
            {
                ViewBag.EmployeeId = employeeId;
                return View("Index", new List<OvertimeDto>());
            }
        }

        public async Task<IActionResult> ByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var overtimes = await httpClient.GetFromJsonAsync<List<OvertimeDto>>($"{_apiBaseUrl}/overtimes/date-range?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                return View("Index", overtimes ?? new List<OvertimeDto>());
            }
            catch
            {
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                return View("Index", new List<OvertimeDto>());
            }
        }

        public async Task<IActionResult> PendingApprovals()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var overtimes = await httpClient.GetFromJsonAsync<List<OvertimeDto>>($"{_apiBaseUrl}/overtimes/pending-approvals");
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
                var httpClient = _httpClientFactory.CreateClient("API");
                var totalHours = await httpClient.GetFromJsonAsync<decimal>($"{_apiBaseUrl}/overtimes/total-hours?employeeId={employeeId}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
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
                var httpClient = _httpClientFactory.CreateClient("API");
                var totalPay = await httpClient.GetFromJsonAsync<decimal>($"{_apiBaseUrl}/overtimes/total-pay?employeeId={employeeId}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
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