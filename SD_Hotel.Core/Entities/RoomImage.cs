namespace SD_Hotel.Core.Entities
{
    public class RoomImage : BaseEntity
    {
        public int RoomId { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public bool IsMainImage { get; set; } = false;
        
        public virtual Room Room { get; set; }
    }
} 