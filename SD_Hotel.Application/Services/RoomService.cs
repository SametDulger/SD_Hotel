using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SD_Hotel.Application.DTOs;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;

namespace SD_Hotel.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<RoomDto?> GetByIdAsync(int id)
        {
            var room = await _roomRepository.GetRoomWithImagesAsync(id);
            if (room == null) return null;

            return new RoomDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Floor = room.Floor,
                RoomType = room.RoomType.ToString(),
                BasePrice = room.BasePrice,
                IsAvailable = room.IsAvailable,
                IsUnderMaintenance = room.IsUnderMaintenance,
                Description = room.Description,
                Capacity = room.Capacity,
                ImageUrls = room.RoomImages?.Select(ri => ri.ImageUrl).ToList() ?? new List<string>()
            };
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                Floor = r.Floor,
                RoomType = r.RoomType.ToString(),
                BasePrice = r.BasePrice,
                IsAvailable = r.IsAvailable,
                IsUnderMaintenance = r.IsUnderMaintenance,
                Description = r.Description,
                Capacity = r.Capacity
            });
        }

        public async Task<RoomDto?> CreateAsync(CreateRoomDto createRoomDto)
        {
            var room = new Room
            {
                RoomNumber = createRoomDto.RoomNumber,
                Floor = createRoomDto.Floor,
                RoomType = Enum.Parse<RoomType>(createRoomDto.RoomType),
                BasePrice = createRoomDto.BasePrice,
                Description = createRoomDto.Description,
                Capacity = createRoomDto.Capacity
            };

            var createdRoom = await _roomRepository.AddAsync(room);
            return await GetByIdAsync(createdRoom.Id);
        }

        public async Task<RoomDto?> UpdateAsync(UpdateRoomDto updateRoomDto)
        {
            var room = await _roomRepository.GetByIdAsync(updateRoomDto.Id);
            if (room == null) return null;

            room.RoomNumber = updateRoomDto.RoomNumber;
            room.Floor = updateRoomDto.Floor;
            room.RoomType = Enum.Parse<RoomType>(updateRoomDto.RoomType);
            room.BasePrice = updateRoomDto.BasePrice;
            room.IsAvailable = updateRoomDto.IsAvailable;
            room.IsUnderMaintenance = updateRoomDto.IsUnderMaintenance;
            room.Description = updateRoomDto.Description;
            room.Capacity = updateRoomDto.Capacity;

            await _roomRepository.UpdateAsync(room);
            return await GetByIdAsync(room.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await _roomRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, string? roomType = null)
        {
            RoomType? roomTypeEnum = null;
            if (!string.IsNullOrEmpty(roomType))
            {
                roomTypeEnum = Enum.Parse<RoomType>(roomType);
            }

            var rooms = await _roomRepository.GetAvailableRoomsAsync(checkIn, checkOut, roomTypeEnum);
            return rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                Floor = r.Floor,
                RoomType = r.RoomType.ToString(),
                BasePrice = r.BasePrice,
                IsAvailable = r.IsAvailable,
                IsUnderMaintenance = r.IsUnderMaintenance,
                Description = r.Description,
                Capacity = r.Capacity
            });
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByTypeAsync(string roomType)
        {
            var roomTypeEnum = Enum.Parse<RoomType>(roomType);
            var rooms = await _roomRepository.GetRoomsByTypeAsync(roomTypeEnum);
            return rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                Floor = r.Floor,
                RoomType = r.RoomType.ToString(),
                BasePrice = r.BasePrice,
                IsAvailable = r.IsAvailable,
                IsUnderMaintenance = r.IsUnderMaintenance,
                Description = r.Description,
                Capacity = r.Capacity
            });
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByFloorAsync(int floor)
        {
            var rooms = await _roomRepository.GetRoomsByFloorAsync(floor);
            return rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                Floor = r.Floor,
                RoomType = r.RoomType.ToString(),
                BasePrice = r.BasePrice,
                IsAvailable = r.IsAvailable,
                IsUnderMaintenance = r.IsUnderMaintenance,
                Description = r.Description,
                Capacity = r.Capacity
            });
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            return await _roomRepository.IsRoomAvailableAsync(roomId, checkIn, checkOut);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsUnderMaintenanceAsync()
        {
            var rooms = await _roomRepository.GetRoomsUnderMaintenanceAsync();
            return rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                Floor = r.Floor,
                RoomType = r.RoomType.ToString(),
                BasePrice = r.BasePrice,
                IsAvailable = r.IsAvailable,
                IsUnderMaintenance = r.IsUnderMaintenance,
                Description = r.Description,
                Capacity = r.Capacity
            });
        }
    }
} 