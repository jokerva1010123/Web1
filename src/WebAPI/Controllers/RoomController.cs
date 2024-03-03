using Microsoft.AspNetCore.Mvc;
using Exceptions;
using Models;
using Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/Rooms/")]
    public class RoomController : Controller
    {
        private readonly RoomServices roomServices;
        public RoomController(RoomServices roomServices)
        {
            this.roomServices = roomServices;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Room>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getAllRooms()
        {
            try
            {
                List<Room> allRooms = await roomServices.getAllRoom();
                return Ok(allRooms);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{id_room}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Room))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getRoom([FromRoute] int id_room)
        {
            try
            {
                Room room = await roomServices.getRoomById(id_room);
                return Ok(room);
            }
            catch (RoomNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
