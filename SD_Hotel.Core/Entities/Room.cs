using System.Collections.Generic;

namespace SD_Hotel.Core.Entities
{
    public class Room : BaseEntity
    {
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public RoomType RoomType { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsUnderMaintenance { get; set; } = false;
        public string Description { get; set; }
        public int Capacity { get; set; }
        
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<RoomImage> RoomImages { get; set; }
    }

    public enum RoomType
    {
        Single = 1,
        Double = 2,
        KingSuite = 3
    }
} 