using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SD_Hotel.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public ReservationsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var reservations = await httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations");
                return View(reservations ?? new List<ReservationDto>());
            }
            catch
            {
                return View(new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var reservation = await httpClient.GetFromJsonAsync<ReservationDto>($"{_apiBaseUrl}/reservations/{id}");
                if (reservation == null)
                    return NotFound();

                return View(reservation);
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
                var rooms = await httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms");
                var customers = await httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers");

                ViewBag.Rooms = rooms ?? new List<RoomDto>();
                ViewBag.Customers = customers ?? new List<CustomerDto>();
            }
            catch
            {
                ViewBag.Rooms = new List<RoomDto>();
                ViewBag.Customers = new List<CustomerDto>();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReservationDto createReservationDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PostAsJsonAsync($"{_apiBaseUrl}/reservations", createReservationDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Rezervasyon oluşturulurken bir hata oluştu.");
                }
            }

            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var rooms = await httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms");
                var customers = await httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers");

                ViewBag.Rooms = rooms ?? new List<RoomDto>();
                ViewBag.Customers = customers ?? new List<CustomerDto>();
            }
            catch
            {
                ViewBag.Rooms = new List<RoomDto>();
                ViewBag.Customers = new List<CustomerDto>();
            }

            return View(createReservationDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var reservation = await httpClient.GetFromJsonAsync<ReservationDto>($"{_apiBaseUrl}/reservations/{id}");
                if (reservation == null)
                    return NotFound();

                var rooms = await httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms");
                var customers = await httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers");

                ViewBag.Rooms = rooms ?? new List<RoomDto>();
                ViewBag.Customers = customers ?? new List<CustomerDto>();

                var updateDto = new UpdateReservationDto
                {
                    Id = reservation.Id,
                    CustomerId = reservation.CustomerId,
                    RoomId = reservation.RoomId,
                    CheckInDate = reservation.CheckInDate,
                    CheckOutDate = reservation.CheckOutDate,
                    NumberOfGuests = reservation.NumberOfGuests,
                    PackageType = reservation.PackageType,
                    TotalPrice = reservation.TotalPrice,
                    Status = reservation.Status,
                    SpecialRequests = reservation.SpecialRequests
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
        public async Task<IActionResult> Edit(int id, UpdateReservationDto updateReservationDto)
        {
            if (id != updateReservationDto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PutAsJsonAsync($"{_apiBaseUrl}/reservations/{id}", updateReservationDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Rezervasyon güncellenirken bir hata oluştu.");
                }
            }

            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var rooms = await httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms");
                var customers = await httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers");

                ViewBag.Rooms = rooms ?? new List<RoomDto>();
                ViewBag.Customers = customers ?? new List<CustomerDto>();
            }
            catch
            {
                ViewBag.Rooms = new List<RoomDto>();
                ViewBag.Customers = new List<CustomerDto>();
            }

            return View(updateReservationDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var reservation = await httpClient.GetFromJsonAsync<ReservationDto>($"{_apiBaseUrl}/reservations/{id}");
                if (reservation == null)
                    return NotFound();

                return View(reservation);
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
                var response = await httpClient.DeleteAsync($"{_apiBaseUrl}/reservations/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Rezervasyon silinirken bir hata oluştu.");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ByCustomer(int customerId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var reservations = await httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/customer/{customerId}");
                ViewBag.CustomerId = customerId;
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.CustomerId = customerId;
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> ByRoom(int roomId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var reservations = await httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/room/{roomId}");
                ViewBag.RoomId = roomId;
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.RoomId = roomId;
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> ByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var reservations = await httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/daterange?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> ByStatus(string status)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var reservations = await httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/status/{status}");
                ViewBag.Status = status;
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.Status = status;
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> TodayCheckIns()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var reservations = await httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/today/checkins");
                ViewBag.Title = "Bugünkü Check-in'ler";
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.Title = "Bugünkü Check-in'ler";
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> TodayCheckOuts()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var reservations = await httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/today/checkouts");
                ViewBag.Title = "Bugünkü Check-out'lar";
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.Title = "Bugünkü Check-out'lar";
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> CheckConflict(int roomId, DateTime checkIn, DateTime checkOut, int? excludeReservationId = null)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var queryParams = $"roomId={roomId}&checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}";
                if (excludeReservationId.HasValue)
                {
                    queryParams += $"&excludeReservationId={excludeReservationId.Value}";
                }

                var hasConflict = await httpClient.GetFromJsonAsync<bool>($"{_apiBaseUrl}/reservations/conflict?{queryParams}");
                ViewBag.RoomId = roomId;
                ViewBag.CheckIn = checkIn;
                ViewBag.CheckOut = checkOut;
                ViewBag.HasConflict = hasConflict;
                return View();
            }
            catch
            {
                ViewBag.RoomId = roomId;
                ViewBag.CheckIn = checkIn;
                ViewBag.CheckOut = checkOut;
                ViewBag.HasConflict = false;
                return View();
            }
        }

        public async Task<IActionResult> CalculatePrice(int roomId, DateTime checkIn, DateTime checkOut, string packageType, int numberOfGuests)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var totalPrice = await httpClient.GetFromJsonAsync<decimal>($"{_apiBaseUrl}/reservations/calculate-price?roomId={roomId}&checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}&packageType={packageType}&numberOfGuests={numberOfGuests}");
                ViewBag.RoomId = roomId;
                ViewBag.CheckIn = checkIn;
                ViewBag.CheckOut = checkOut;
                ViewBag.PackageType = packageType;
                ViewBag.NumberOfGuests = numberOfGuests;
                ViewBag.TotalPrice = totalPrice;
                return View();
            }
            catch
            {
                ViewBag.RoomId = roomId;
                ViewBag.CheckIn = checkIn;
                ViewBag.CheckOut = checkOut;
                ViewBag.PackageType = packageType;
                ViewBag.NumberOfGuests = numberOfGuests;
                ViewBag.TotalPrice = 0;
                return View();
            }
        }
    }
} 