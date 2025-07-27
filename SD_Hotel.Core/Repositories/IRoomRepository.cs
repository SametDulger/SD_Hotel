using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Hotel.Core.Entities;

namespace SD_Hotel.Core.Repositories
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, RoomType? roomType = null);
        Task<IEnumerable<Room>> GetRoomsByTypeAsync(RoomType roomType);
        Task<IEnumerable<Room>> GetRoomsByFloorAsync(int floor);
        Task<Room?> GetRoomWithImagesAsync(int id);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<Room>> GetRoomsUnderMaintenanceAsync();
    }
} 