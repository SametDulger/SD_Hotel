using Microsoft.EntityFrameworkCore;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;
using SD_Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SD_Hotel.Infrastructure.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(SD_HotelDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, RoomType? roomType = null)
        {
            var query = _dbSet.Where(r => r.IsAvailable && !r.IsUnderMaintenance && r.IsActive);

            if (roomType.HasValue)
            {
                query = query.Where(r => r.RoomType == roomType.Value);
            }

            var conflictingReservations = await _context.Reservations
                .Where(r => r.Status != ReservationStatus.Cancelled && r.IsActive &&
                           ((r.CheckInDate <= checkIn && r.CheckOutDate > checkIn) ||
                            (r.CheckInDate < checkOut && r.CheckOutDate >= checkOut) ||
                            (r.CheckInDate >= checkIn && r.CheckOutDate <= checkOut)))
                .Select(r => r.RoomId)
                .ToListAsync();

            return await query.Where(r => !conflictingReservations.Contains(r.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetRoomsByTypeAsync(RoomType roomType)
        {
            return await _dbSet.Where(r => r.RoomType == roomType && r.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetRoomsByFloorAsync(int floor)
        {
            return await _dbSet.Where(r => r.Floor == floor && r.IsActive).ToListAsync();
        }

        public async Task<Room> GetRoomWithImagesAsync(int id)
        {
            return await _dbSet.Include(r => r.RoomImages).FirstOrDefaultAsync(r => r.Id == id && r.IsActive);
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = await _dbSet.FirstOrDefaultAsync(r => r.Id == roomId && r.IsActive);
            if (room == null || !room.IsAvailable || room.IsUnderMaintenance)
                return false;

            var conflictingReservation = await _context.Reservations
                .AnyAsync(r => r.RoomId == roomId && r.Status != ReservationStatus.Cancelled && r.IsActive &&
                              ((r.CheckInDate <= checkIn && r.CheckOutDate > checkIn) ||
                               (r.CheckInDate < checkOut && r.CheckOutDate >= checkOut) ||
                               (r.CheckInDate >= checkIn && r.CheckOutDate <= checkOut)));

            return !conflictingReservation;
        }

        public async Task<IEnumerable<Room>> GetRoomsUnderMaintenanceAsync()
        {
            return await _dbSet.Where(r => r.IsUnderMaintenance && r.IsActive).ToListAsync();
        }
    }
} 