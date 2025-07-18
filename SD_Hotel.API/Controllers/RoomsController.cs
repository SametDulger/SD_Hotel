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
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll()
        {
            var rooms = await _roomService.GetAllAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetById(int id)
        {
            var room = await _roomService.GetByIdAsync(id);
            if (room == null)
                return NotFound();

            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> Create(CreateRoomDto createRoomDto)
        {
            var room = await _roomService.CreateAsync(createRoomDto);
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomDto>> Update(int id, UpdateRoomDto updateRoomDto)
        {
            if (id != updateRoomDto.Id)
                return BadRequest();

            var room = await _roomService.UpdateAsync(updateRoomDto);
            if (room == null)
                return NotFound();

            return Ok(room);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _roomService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAvailableRooms(
            [FromQuery] DateTime checkIn, 
            [FromQuery] DateTime checkOut, 
            [FromQuery] string roomType = null)
        {
            var rooms = await _roomService.GetAvailableRoomsAsync(checkIn, checkOut, roomType);
            return Ok(rooms);
        }

        [HttpGet("type/{roomType}")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetByType(string roomType)
        {
            var rooms = await _roomService.GetRoomsByTypeAsync(roomType);
            return Ok(rooms);
        }

        [HttpGet("floor/{floor}")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetByFloor(int floor)
        {
            var rooms = await _roomService.GetRoomsByFloorAsync(floor);
            return Ok(rooms);
        }

        [HttpGet("{id}/available")]
        public async Task<ActionResult<bool>> IsRoomAvailable(
            int id, 
            [FromQuery] DateTime checkIn, 
            [FromQuery] DateTime checkOut)
        {
            var isAvailable = await _roomService.IsRoomAvailableAsync(id, checkIn, checkOut);
            return Ok(isAvailable);
        }

        [HttpGet("maintenance")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRoomsUnderMaintenance()
        {
            var rooms = await _roomService.GetRoomsUnderMaintenanceAsync();
            return Ok(rooms);
        }
    }
} 