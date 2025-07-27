using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SD_Hotel.Application.DTOs;
using SD_Hotel.Core.Entities;
using SD_Hotel.Core.Repositories;

namespace SD_Hotel.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public async Task<ReservationDto?> GetByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetReservationWithDetailsAsync(id);
            if (reservation == null) return null;

            return new ReservationDto
            {
                Id = reservation.Id,
                CustomerId = reservation.CustomerId,
                CustomerName = $"{reservation.Customer.FirstName} {reservation.Customer.LastName}",
                RoomId = reservation.RoomId,
                RoomNumber = reservation.Room.RoomNumber,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                NumberOfGuests = reservation.NumberOfGuests,
                PackageType = reservation.PackageType.ToString(),
                TotalPrice = reservation.TotalPrice,
                Status = reservation.Status.ToString(),
                SpecialRequests = reservation.SpecialRequests,
                ConfirmationDate = reservation.ConfirmationDate,
                IsEarlyReservation = reservation.IsEarlyReservation,
                EarlyReservationDiscount = reservation.EarlyReservationDiscount
            };
        }

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                RoomId = r.RoomId,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                PackageType = r.PackageType.ToString(),
                TotalPrice = r.TotalPrice,
                Status = r.Status.ToString(),
                SpecialRequests = r.SpecialRequests,
                ConfirmationDate = r.ConfirmationDate,
                IsEarlyReservation = r.IsEarlyReservation,
                EarlyReservationDiscount = r.EarlyReservationDiscount
            });
        }

        public async Task<ReservationDto?> CreateAsync(CreateReservationDto createReservationDto)
        {
            var totalPrice = await CalculateTotalPriceAsync(
                createReservationDto.RoomId,
                createReservationDto.CheckInDate,
                createReservationDto.CheckOutDate,
                createReservationDto.PackageType,
                createReservationDto.NumberOfGuests);

            var reservation = new Reservation
            {
                CustomerId = createReservationDto.CustomerId,
                RoomId = createReservationDto.RoomId,
                CheckInDate = createReservationDto.CheckInDate,
                CheckOutDate = createReservationDto.CheckOutDate,
                NumberOfGuests = createReservationDto.NumberOfGuests,
                PackageType = Enum.Parse<PackageType>(createReservationDto.PackageType),
                TotalPrice = totalPrice,
                Status = ReservationStatus.Pending,
                SpecialRequests = createReservationDto.SpecialRequests
            };

            var createdReservation = await _reservationRepository.AddAsync(reservation);
            return await GetByIdAsync(createdReservation.Id);
        }

        public async Task<ReservationDto?> UpdateAsync(UpdateReservationDto updateReservationDto)
        {
            var reservation = await _reservationRepository.GetByIdAsync(updateReservationDto.Id);
            if (reservation == null) return null;

            reservation.CustomerId = updateReservationDto.CustomerId;
            reservation.RoomId = updateReservationDto.RoomId;
            reservation.CheckInDate = updateReservationDto.CheckInDate;
            reservation.CheckOutDate = updateReservationDto.CheckOutDate;
            reservation.NumberOfGuests = updateReservationDto.NumberOfGuests;
            reservation.PackageType = Enum.Parse<PackageType>(updateReservationDto.PackageType);
            reservation.TotalPrice = updateReservationDto.TotalPrice;
            reservation.Status = Enum.Parse<ReservationStatus>(updateReservationDto.Status);
            reservation.SpecialRequests = updateReservationDto.SpecialRequests;

            await _reservationRepository.UpdateAsync(reservation);
            return await GetByIdAsync(reservation.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await _reservationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ReservationDto>> GetByCustomerAsync(int customerId)
        {
            var reservations = await _reservationRepository.GetReservationsByCustomerAsync(customerId);
            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                PackageType = r.PackageType.ToString(),
                TotalPrice = r.TotalPrice,
                Status = r.Status.ToString(),
                SpecialRequests = r.SpecialRequests,
                ConfirmationDate = r.ConfirmationDate,
                IsEarlyReservation = r.IsEarlyReservation,
                EarlyReservationDiscount = r.EarlyReservationDiscount
            });
        }

        public async Task<IEnumerable<ReservationDto>> GetByRoomAsync(int roomId)
        {
            var reservations = await _reservationRepository.GetReservationsByRoomAsync(roomId);
            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                PackageType = r.PackageType.ToString(),
                TotalPrice = r.TotalPrice,
                Status = r.Status.ToString(),
                SpecialRequests = r.SpecialRequests,
                ConfirmationDate = r.ConfirmationDate,
                IsEarlyReservation = r.IsEarlyReservation,
                EarlyReservationDiscount = r.EarlyReservationDiscount
            });
        }

        public async Task<IEnumerable<ReservationDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var reservations = await _reservationRepository.GetReservationsByDateRangeAsync(startDate, endDate);
            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                PackageType = r.PackageType.ToString(),
                TotalPrice = r.TotalPrice,
                Status = r.Status.ToString(),
                SpecialRequests = r.SpecialRequests,
                ConfirmationDate = r.ConfirmationDate,
                IsEarlyReservation = r.IsEarlyReservation,
                EarlyReservationDiscount = r.EarlyReservationDiscount
            });
        }

        public async Task<IEnumerable<ReservationDto>> GetByStatusAsync(string status)
        {
            var statusEnum = Enum.Parse<ReservationStatus>(status);
            var reservations = await _reservationRepository.GetReservationsByStatusAsync(statusEnum);
            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                PackageType = r.PackageType.ToString(),
                TotalPrice = r.TotalPrice,
                Status = r.Status.ToString(),
                SpecialRequests = r.SpecialRequests,
                ConfirmationDate = r.ConfirmationDate,
                IsEarlyReservation = r.IsEarlyReservation,
                EarlyReservationDiscount = r.EarlyReservationDiscount
            });
        }

        public async Task<IEnumerable<ReservationDto>> GetTodayCheckInsAsync()
        {
            var reservations = await _reservationRepository.GetTodayCheckInsAsync();
            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                PackageType = r.PackageType.ToString(),
                TotalPrice = r.TotalPrice,
                Status = r.Status.ToString(),
                SpecialRequests = r.SpecialRequests,
                ConfirmationDate = r.ConfirmationDate,
                IsEarlyReservation = r.IsEarlyReservation,
                EarlyReservationDiscount = r.EarlyReservationDiscount
            });
        }

        public async Task<IEnumerable<ReservationDto>> GetTodayCheckOutsAsync()
        {
            var reservations = await _reservationRepository.GetTodayCheckOutsAsync();
            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
                RoomId = r.RoomId,
                RoomNumber = r.Room.RoomNumber,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfGuests = r.NumberOfGuests,
                PackageType = r.PackageType.ToString(),
                TotalPrice = r.TotalPrice,
                Status = r.Status.ToString(),
                SpecialRequests = r.SpecialRequests,
                ConfirmationDate = r.ConfirmationDate,
                IsEarlyReservation = r.IsEarlyReservation,
                EarlyReservationDiscount = r.EarlyReservationDiscount
            });
        }

        public async Task<bool> HasConflictingReservationAsync(int roomId, DateTime checkIn, DateTime checkOut, int? excludeReservationId = null)
        {
            return await _reservationRepository.HasConflictingReservationAsync(roomId, checkIn, checkOut, excludeReservationId);
        }

        public async Task<decimal> CalculateTotalPriceAsync(int roomId, DateTime checkIn, DateTime checkOut, string packageType, int numberOfGuests)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            if (room == null) return 0;

            var days = (checkOut - checkIn).Days;
            var basePrice = room.BasePrice * days;

            var packageMultiplier = packageType switch
            {
                "RoomOnly" => 1.0m,
                "HalfBoard" => 1.3m,
                "FullBoard" => 1.6m,
                "AllInclusive" => 2.0m,
                _ => 1.0m
            };

            var totalPrice = basePrice * packageMultiplier;

            var earlyReservationDiscount = 0.0m;
            var daysUntilCheckIn = (checkIn - DateTime.Today).Days;

            if (daysUntilCheckIn >= 90)
            {
                earlyReservationDiscount = packageType == "AllInclusive" ? 0.23m : 0.23m;
            }
            else if (daysUntilCheckIn >= 30)
            {
                earlyReservationDiscount = packageType == "AllInclusive" ? 0.18m : 0.16m;
            }

            totalPrice = totalPrice * (1 - earlyReservationDiscount);

            return totalPrice;
        }
    }
} 