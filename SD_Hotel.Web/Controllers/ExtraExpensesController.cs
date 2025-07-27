using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SD_Hotel.Web.Controllers
{
    public class ExtraExpensesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public ExtraExpensesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var expenses = await httpClient.GetFromJsonAsync<List<ExtraExpenseDto>>($"{_apiBaseUrl}/extraexpenses");
                return View(expenses ?? new List<ExtraExpenseDto>());
            }
            catch
            {
                return View(new List<ExtraExpenseDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var expense = await httpClient.GetFromJsonAsync<ExtraExpenseDto>($"{_apiBaseUrl}/extraexpenses/{id}");
                if (expense == null)
                    return NotFound();

                return View(expense);
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
        public async Task<IActionResult> Create(CreateExtraExpenseDto createExtraExpenseDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PostAsJsonAsync($"{_apiBaseUrl}/extraexpenses", createExtraExpenseDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Ek gider oluşturulurken bir hata oluştu.");
                }
            }
            return View(createExtraExpenseDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var expense = await httpClient.GetFromJsonAsync<ExtraExpenseDto>($"{_apiBaseUrl}/extraexpenses/{id}");
                if (expense == null)
                    return NotFound();

                var updateDto = new UpdateExtraExpenseDto
                {
                    Id = expense.Id,
                    CustomerId = expense.CustomerId,
                    CustomerName = expense.CustomerName,
                    ReservationId = expense.ReservationId,
                    ExpenseType = expense.ExpenseType,
                    Description = expense.Description,
                    Amount = expense.Amount,
                    ExpenseDate = expense.ExpenseDate,
                    IsPaid = expense.IsPaid,
                    PaymentDate = expense.PaymentDate
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
        public async Task<IActionResult> Edit(int id, UpdateExtraExpenseDto updateExtraExpenseDto)
        {
            if (id != updateExtraExpenseDto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PutAsJsonAsync($"{_apiBaseUrl}/extraexpenses/{id}", updateExtraExpenseDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Ek gider güncellenirken bir hata oluştu.");
                }
            }
            return View(updateExtraExpenseDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var expense = await httpClient.GetFromJsonAsync<ExtraExpenseDto>($"{_apiBaseUrl}/extraexpenses/{id}");
                if (expense == null)
                    return NotFound();

                return View(expense);
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
                var response = await httpClient.DeleteAsync($"{_apiBaseUrl}/extraexpenses/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Ek gider silinirken bir hata oluştu.");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ByType(string expenseType)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var expenses = await httpClient.GetFromJsonAsync<List<ExtraExpenseDto>>($"{_apiBaseUrl}/extraexpenses/type/{expenseType}");
                ViewBag.ExpenseType = expenseType;
                return View("Index", expenses ?? new List<ExtraExpenseDto>());
            }
            catch
            {
                ViewBag.ExpenseType = expenseType;
                return View("Index", new List<ExtraExpenseDto>());
            }
        }

        public async Task<IActionResult> ByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var expenses = await httpClient.GetFromJsonAsync<List<ExtraExpenseDto>>($"{_apiBaseUrl}/extraexpenses/date-range?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                return View("Index", expenses ?? new List<ExtraExpenseDto>());
            }
            catch
            {
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                return View("Index", new List<ExtraExpenseDto>());
            }
        }

        public async Task<IActionResult> Total(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var queryParams = "";
                if (startDate.HasValue)
                {
                    queryParams += $"startDate={startDate.Value:yyyy-MM-dd}";
                }
                if (endDate.HasValue)
                {
                    if (!string.IsNullOrEmpty(queryParams))
                        queryParams += "&";
                    queryParams += $"endDate={endDate.Value:yyyy-MM-dd}";
                }

                var total = await httpClient.GetFromJsonAsync<decimal>($"{_apiBaseUrl}/extraexpenses/total?{queryParams}");
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.Total = total;
                return View();
            }
            catch
            {
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.Total = 0;
                return View();
            }
        }
    }
} 