using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SD_Hotel.Web.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "http://localhost:5158/api";

        public RoomsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var rooms = await httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms");
                return View(rooms ?? new List<RoomDto>());
            }
            catch
            {
                return View(new List<RoomDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var room = await httpClient.GetFromJsonAsync<RoomDto>($"{_apiBaseUrl}/rooms/{id}");
                if (room == null)
                    return NotFound();

                return View(room);
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
        public async Task<IActionResult> Create(CreateRoomDto createRoomDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PostAsJsonAsync($"{_apiBaseUrl}/rooms", createRoomDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Oda oluşturulurken bir hata oluştu.");
                }
            }
            return View(createRoomDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var room = await httpClient.GetFromJsonAsync<RoomDto>($"{_apiBaseUrl}/rooms/{id}");
                if (room == null)
                    return NotFound();

                var updateDto = new UpdateRoomDto
                {
                    Id = room.Id,
                    RoomNumber = room.RoomNumber,
                    Floor = room.Floor,
                    RoomType = room.RoomType,
                    BasePrice = room.BasePrice,
                    IsAvailable = room.IsAvailable,
                    IsUnderMaintenance = room.IsUnderMaintenance,
                    Description = room.Description,
                    Capacity = room.Capacity
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
        public async Task<IActionResult> Edit(int id, UpdateRoomDto updateRoomDto)
        {
            if (id != updateRoomDto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("API");
                    var response = await httpClient.PutAsJsonAsync($"{_apiBaseUrl}/rooms/{id}", updateRoomDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Oda güncellenirken bir hata oluştu.");
                }
            }
            return View(updateRoomDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var room = await httpClient.GetFromJsonAsync<RoomDto>($"{_apiBaseUrl}/rooms/{id}");
                if (room == null)
                    return NotFound();

                return View(room);
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
                var response = await httpClient.DeleteAsync($"{_apiBaseUrl}/rooms/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Oda silinirken bir hata oluştu.");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AvailableRooms(DateTime? checkIn, DateTime? checkOut, string? roomType = null)
        {
            if (!checkIn.HasValue || !checkOut.HasValue)
            {
                checkIn = DateTime.Today;
                checkOut = DateTime.Today.AddDays(1);
            }

            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var queryParams = $"checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}";
                if (!string.IsNullOrEmpty(roomType))
                {
                    queryParams += $"&roomType={roomType}";
                }

                var rooms = await httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms/available?{queryParams}");
                ViewBag.CheckIn = checkIn;
                ViewBag.CheckOut = checkOut;
                ViewBag.RoomType = roomType;
                return View(rooms ?? new List<RoomDto>());
            }
            catch
            {
                ViewBag.CheckIn = checkIn;
                ViewBag.CheckOut = checkOut;
                ViewBag.RoomType = roomType;
                return View(new List<RoomDto>());
            }
        }

        public async Task<IActionResult> Maintenance()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var rooms = await httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms/maintenance");
                return View(rooms ?? new List<RoomDto>());
            }
            catch
            {
                return View(new List<RoomDto>());
            }
        }

        public async Task<IActionResult> ByType(string roomType)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var rooms = await httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms/type/{roomType}");
                ViewBag.RoomType = roomType;
                return View("Index", rooms ?? new List<RoomDto>());
            }
            catch
            {
                ViewBag.RoomType = roomType;
                return View("Index", new List<RoomDto>());
            }
        }

        public async Task<IActionResult> ByFloor(int floor)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var rooms = await httpClient.GetFromJsonAsync<List<RoomDto>>($"{_apiBaseUrl}/rooms/floor/{floor}");
                ViewBag.Floor = floor;
                return View("Index", rooms ?? new List<RoomDto>());
            }
            catch
            {
                ViewBag.Floor = floor;
                return View("Index", new List<RoomDto>());
            }
        }

        public async Task<IActionResult> IsAvailable(int id, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("API");
                var isAvailable = await httpClient.GetFromJsonAsync<bool>($"{_apiBaseUrl}/rooms/{id}/available?checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}");
                ViewBag.RoomId = id;
                ViewBag.CheckIn = checkIn;
                ViewBag.CheckOut = checkOut;
                ViewBag.IsAvailable = isAvailable;
                return View();
            }
            catch
            {
                ViewBag.RoomId = id;
                ViewBag.CheckIn = checkIn;
                ViewBag.CheckOut = checkOut;
                ViewBag.IsAvailable = false;
                return View();
            }
        }
    }
} 