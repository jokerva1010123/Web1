using Exceptions;
using ExtraModels;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace WebAPI.Controllers
{
    [Route("api/Things/")]
    [ApiController]
    public class ThingController : Controller
    {
        private readonly ThingServices thingServices;
        private readonly StudentServices studentServices;
        public ThingController(ThingServices thingServices, StudentServices studentServices)
        {
            this.thingServices = thingServices;
            this.studentServices = studentServices;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Thing>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getAllThing([FromQuery] int? id_student)
        {
            try
            {
                List<Thing> allThings = await thingServices.getAllThing();

                if (id_student == null)
                    return StatusCode(StatusCodes.Status200OK, allThings);
                Student student = await studentServices.getStudentById(id_student.Value);
                List<Thing> res = new List<Thing>();
                foreach (Thing thing in allThings)
                    if (thing.Id_student == id_student)
                        res.Add(thing);
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{codeThing}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Thing))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getThing([FromRoute] string codeThing)
        {
            try
            {
                Thing thing = await thingServices.getThing(codeThing);
                return StatusCode(StatusCodes.Status200OK, thing);
            }
            catch (InputInvalidException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (ThingNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> addThing([FromBody] AddNewThing newThing)
        {
            try
            {
                await thingServices.addThing(new Thing { Code = newThing.CodeThing, Type = newThing.Type, Id_room = 1, Id_student = 0 });
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (InputInvalidException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (ThingCodeExistsException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPatch("changeRoom/{codeThing}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> changeRoomThing([FromRoute] string codeThing, [FromBody] id_st id_room)
        {
            try
            {
                await thingServices.changeRoomThing(codeThing, Convert.ToInt32(id_room.Id));
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (InputInvalidException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (ThingNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (RoomNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (ThingInRoomException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPatch("changeStudent/{codeThing}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> changeStudentThing([FromRoute] string codeThing, [FromBody] id_st id_student)
        {
            try
            {
                await thingServices.changeStudentThing(codeThing, Convert.ToInt32(id_student.Id));
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (InputInvalidException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (ThingNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (StudentNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (ThingNotFreeException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }
            catch (ThingFreeException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
