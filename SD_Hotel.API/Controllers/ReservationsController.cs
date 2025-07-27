using Microsoft.AspNetCore.Mvc;
using SD_Hotel.Application.DTOs;
using SD_Hotel.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Hotel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAll()
        {
            var reservations = await _reservationService.GetAllAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetById(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDto>> Create(CreateReservationDto createReservationDto)
        {
            var reservation = await _reservationService.CreateAsync(createReservationDto);
            if (reservation == null)
                return BadRequest();
            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationDto>> Update(int id, UpdateReservationDto updateReservationDto)
        {
            if (id != updateReservationDto.Id)
                return BadRequest();

            var reservation = await _reservationService.UpdateAsync(updateReservationDto);
            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _reservationService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetByCustomer(int customerId)
        {
            var reservations = await _reservationService.GetByCustomerAsync(customerId);
            return Ok(reservations);
        }

        [HttpGet("room/{roomId}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetByRoom(int roomId)
        {
            var reservations = await _reservationService.GetByRoomAsync(roomId);
            return Ok(reservations);
        }

        [HttpGet("daterange")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetByDateRange(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            var reservations = await _reservationService.GetByDateRangeAsync(startDate, endDate);
            return Ok(reservations);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetByStatus(string status)
        {
            var reservations = await _reservationService.GetByStatusAsync(status);
            return Ok(reservations);
        }

        [HttpGet("today/checkins")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetTodayCheckIns()
        {
            var reservations = await _reservationService.GetTodayCheckInsAsync();
            return Ok(reservations);
        }

        [HttpGet("today/checkouts")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetTodayCheckOuts()
        {
            var reservations = await _reservationService.GetTodayCheckOutsAsync();
            return Ok(reservations);
        }

        [HttpGet("conflict")]
        public async Task<ActionResult<bool>> HasConflictingReservation(
            [FromQuery] int roomId, 
            [FromQuery] DateTime checkIn, 
            [FromQuery] DateTime checkOut, 
            [FromQuery] int? excludeReservationId = null)
        {
            var hasConflict = await _reservationService.HasConflictingReservationAsync(roomId, checkIn, checkOut, excludeReservationId);
            return Ok(hasConflict);
        }

        [HttpGet("calculate-price")]
        public async Task<ActionResult<decimal>> CalculateTotalPrice(
            [FromQuery] int roomId, 
            [FromQuery] DateTime checkIn, 
            [FromQuery] DateTime checkOut, 
            [FromQuery] string packageType, 
            [FromQuery] int numberOfGuests)
        {
            var totalPrice = await _reservationService.CalculateTotalPriceAsync(roomId, checkIn, checkOut, packageType, numberOfGuests);
            return Ok(totalPrice);
        }
    }
} 