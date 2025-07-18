namespace SD_Hotel.Web.Models
{
    public class DashboardViewModel
    {
        public int TotalRooms { get; set; }
        public int ActiveReservations { get; set; }
        public int TotalCustomers { get; set; }
        public int ActiveEmployees { get; set; }
        public List<ActivityItem> RecentActivities { get; set; } = new List<ActivityItem>();
    }

    public class ActivityItem
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    public class ReservationDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
} 