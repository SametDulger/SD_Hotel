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
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public ReservationsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var reservations = await _httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations");
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
                var reservation = await _httpClient.GetFromJsonAsync<ReservationDto>($"{_apiBaseUrl}/reservations/{id}");
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
                var rooms = await _httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms");
                var customers = await _httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers");
                
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
                    var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/reservations", createReservationDto);
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
                var rooms = await _httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms");
                var customers = await _httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers");
                
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
                var reservation = await _httpClient.GetFromJsonAsync<ReservationDto>($"{_apiBaseUrl}/reservations/{id}");
                if (reservation == null)
                    return NotFound();

                var rooms = await _httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms");
                var customers = await _httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers");

                var updateDto = new UpdateReservationDto
                {
                    Id = reservation.Id,
                    CustomerId = reservation.CustomerId,
                    RoomId = reservation.RoomId,
                    CheckInDate = reservation.CheckInDate,
                    CheckOutDate = reservation.CheckOutDate,
                    NumberOfGuests = reservation.NumberOfGuests,
                    PackageType = reservation.PackageType,
                    Status = reservation.Status,
                    TotalPrice = reservation.TotalPrice,
                    Notes = reservation.Notes
                };

                ViewBag.Rooms = rooms ?? new List<RoomDto>();
                ViewBag.Customers = customers ?? new List<CustomerDto>();

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
                    var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/reservations/{id}", updateReservationDto);
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
                var rooms = await _httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms");
                var customers = await _httpClient.GetFromJsonAsync<List<CustomerDto>>($"{_apiBaseUrl}/customers");
                
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
                var reservation = await _httpClient.GetFromJsonAsync<ReservationDto>($"{_apiBaseUrl}/reservations/{id}");
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
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/reservations/{id}");
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
                var reservations = await _httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/customer/{customerId}");
                ViewBag.CustomerId = customerId;
                ViewBag.Title = $"Müşteri ID: {customerId} Rezervasyonları";
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.CustomerId = customerId;
                ViewBag.Title = $"Müşteri ID: {customerId} Rezervasyonları";
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> ByRoom(int roomId)
        {
            try
            {
                var reservations = await _httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/room/{roomId}");
                ViewBag.RoomId = roomId;
                ViewBag.Title = $"Oda ID: {roomId} Rezervasyonları";
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.RoomId = roomId;
                ViewBag.Title = $"Oda ID: {roomId} Rezervasyonları";
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> ByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var reservations = await _httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/daterange?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.Title = $"Tarih Aralığı: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}";
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;
                ViewBag.Title = $"Tarih Aralığı: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}";
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> ByStatus(string status)
        {
            try
            {
                var reservations = await _httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/status/{status}");
                ViewBag.Status = status;
                ViewBag.Title = $"Durum: {status}";
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.Status = status;
                ViewBag.Title = $"Durum: {status}";
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> TodayCheckIns()
        {
            try
            {
                var reservations = await _httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/today/checkins");
                ViewBag.Title = "Bugün Giriş Yapacaklar";
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.Title = "Bugün Giriş Yapacaklar";
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> TodayCheckOuts()
        {
            try
            {
                var reservations = await _httpClient.GetFromJsonAsync<List<ReservationDto>>($"{_apiBaseUrl}/reservations/today/checkouts");
                ViewBag.Title = "Bugün Çıkış Yapacaklar";
                return View("Index", reservations ?? new List<ReservationDto>());
            }
            catch
            {
                ViewBag.Title = "Bugün Çıkış Yapacaklar";
                return View("Index", new List<ReservationDto>());
            }
        }

        public async Task<IActionResult> CheckConflict(int roomId, DateTime checkIn, DateTime checkOut, int? excludeReservationId = null)
        {
            try
            {
                var queryParams = $"roomId={roomId}&checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}";
                if (excludeReservationId.HasValue)
                {
                    queryParams += $"&excludeReservationId={excludeReservationId.Value}";
                }

                var hasConflict = await _httpClient.GetFromJsonAsync<bool>($"{_apiBaseUrl}/reservations/conflict?{queryParams}");
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
                ViewBag.HasConflict = true;
                return View();
            }
        }

        public async Task<IActionResult> CalculatePrice(int roomId, DateTime checkIn, DateTime checkOut, string packageType, int numberOfGuests)
        {
            try
            {
                var totalPrice = await _httpClient.GetFromJsonAsync<decimal>($"{_apiBaseUrl}/reservations/calculate-price?roomId={roomId}&checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}&packageType={packageType}&numberOfGuests={numberOfGuests}");
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