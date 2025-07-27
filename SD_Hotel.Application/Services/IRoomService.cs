using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Hotel.Application.DTOs;

namespace SD_Hotel.Application.Services
{
    public interface IRoomService
    {
        Task<RoomDto?> GetByIdAsync(int id);
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task<RoomDto?> CreateAsync(CreateRoomDto createRoomDto);
        Task<RoomDto?> UpdateAsync(UpdateRoomDto updateRoomDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, string? roomType = null);
        Task<IEnumerable<RoomDto>> GetRoomsByTypeAsync(string roomType);
        Task<IEnumerable<RoomDto>> GetRoomsByFloorAsync(int floor);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);
        Task<IEnumerable<RoomDto>> GetRoomsUnderMaintenanceAsync();
    }
} 