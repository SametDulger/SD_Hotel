using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace SD_Hotel.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("API");
            
            var dashboardData = new DashboardViewModel
            {
                TotalRooms = await GetTotalRooms(httpClient),
                ActiveReservations = await GetActiveReservations(httpClient),
                TotalCustomers = await GetTotalCustomers(httpClient),
                ActiveEmployees = await GetActiveEmployees(httpClient),
                RecentActivities = await GetRecentActivities(httpClient)
            };

            return View(dashboardData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Dashboard verileri yüklenirken hata oluştu");
            return View(new DashboardViewModel());
        }
    }

    private async Task<int> GetTotalRooms(HttpClient httpClient)
    {
        try
        {
            var response = await httpClient.GetAsync("api/rooms");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var rooms = JsonSerializer.Deserialize<List<object>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return rooms?.Count ?? 0;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Oda sayısı alınırken hata oluştu");
        }
        return 0;
    }

    private async Task<int> GetActiveReservations(HttpClient httpClient)
    {
        try
        {
            var response = await httpClient.GetAsync("api/reservations");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var reservations = JsonSerializer.Deserialize<List<object>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return reservations?.Count ?? 0;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Aktif rezervasyon sayısı alınırken hata oluştu");
        }
        return 0;
    }

    private async Task<int> GetTotalCustomers(HttpClient httpClient)
    {
        try
        {
            var response = await httpClient.GetAsync("api/customers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customers = JsonSerializer.Deserialize<List<object>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return customers?.Count ?? 0;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Müşteri sayısı alınırken hata oluştu");
        }
        return 0;
    }

    private async Task<int> GetActiveEmployees(HttpClient httpClient)
    {
        try
        {
            var response = await httpClient.GetAsync("api/employees/active");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employees = JsonSerializer.Deserialize<List<object>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return employees?.Count ?? 0;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Aktif personel sayısı alınırken hata oluştu");
        }
        return 0;
    }

    private async Task<List<ActivityItem>> GetRecentActivities(HttpClient httpClient)
    {
        var activities = new List<ActivityItem>();
        
        try
        {
            // Son rezervasyonları al
            var reservationsResponse = await httpClient.GetAsync("api/reservations");
            if (reservationsResponse.IsSuccessStatusCode)
            {
                var content = await reservationsResponse.Content.ReadAsStringAsync();
                var reservations = JsonSerializer.Deserialize<List<ReservationDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (reservations?.Any() == true)
                {
                    var latestReservation = reservations.OrderByDescending(r => r.CreatedAt).First();
                    activities.Add(new ActivityItem
                    {
                        Title = "Yeni Rezervasyon",
                        Description = $"Oda {latestReservation.RoomNumber} için yeni rezervasyon oluşturuldu",
                        TimeAgo = GetTimeAgo(latestReservation.CreatedAt),
                        Type = "success"
                    });
                }
            }

            // Son müşteri kayıtlarını al
            var customersResponse = await httpClient.GetAsync("api/customers");
            if (customersResponse.IsSuccessStatusCode)
            {
                var content = await customersResponse.Content.ReadAsStringAsync();
                var customers = JsonSerializer.Deserialize<List<CustomerDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (customers?.Any() == true)
                {
                    var latestCustomer = customers.OrderByDescending(c => c.CreatedAt).First();
                    activities.Add(new ActivityItem
                    {
                        Title = "Müşteri Kaydı",
                        Description = $"{latestCustomer.FirstName} {latestCustomer.LastName} sisteme kaydedildi",
                        TimeAgo = GetTimeAgo(latestCustomer.CreatedAt),
                        Type = "info"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Son aktiviteler alınırken hata oluştu");
        }

        // Eğer gerçek veri yoksa varsayılan aktiviteler
        if (!activities.Any())
        {
            activities.AddRange(new List<ActivityItem>
            {
                new ActivityItem { Title = "Sistem Başlatıldı", Description = "SD Hotel yönetim sistemi başarıyla başlatıldı", TimeAgo = "Az önce", Type = "primary" },
                new ActivityItem { Title = "Veritabanı Bağlantısı", Description = "Veritabanı bağlantısı başarıyla kuruldu", TimeAgo = "1 dakika önce", Type = "success" }
            });
        }

        return activities.OrderByDescending(a => a.TimeAgo).Take(5).ToList();
    }

    private string GetTimeAgo(DateTime? dateTime)
    {
        if (!dateTime.HasValue) return "Bilinmiyor";
        
        var timeSpan = DateTime.Now - dateTime.Value;
        
        if (timeSpan.TotalMinutes < 1)
            return "Az önce";
        else if (timeSpan.TotalMinutes < 60)
            return $"{(int)timeSpan.TotalMinutes} dakika önce";
        else if (timeSpan.TotalHours < 24)
            return $"{(int)timeSpan.TotalHours} saat önce";
        else
            return $"{(int)timeSpan.TotalDays} gün önce";
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
