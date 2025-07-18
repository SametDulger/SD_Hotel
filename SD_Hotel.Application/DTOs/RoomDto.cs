using System;
using System.Collections.Generic;

namespace SD_Hotel.Application.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public string RoomType { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsUnderMaintenance { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class CreateRoomDto
    {
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public string RoomType { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsUnderMaintenance { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
    }

    public class UpdateRoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public string RoomType { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsUnderMaintenance { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
    }
} 