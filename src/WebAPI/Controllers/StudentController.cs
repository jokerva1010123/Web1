using Microsoft.AspNetCore.Mvc;
using Exceptions;
using Models;
using Services;
using ExtraModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace WebAPI.Controllers
{
    [Route("api/Students/")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly StudentServices studentServices;
        private readonly UserServices userServices;
        public StudentController(StudentServices studentServices, UserServices userServices)
        {
            this.studentServices = studentServices;
            this.userServices = userServices;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Student>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getAllStudents([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            try
            {
                if (page == null) page = 1;
                if (pageSize == null) pageSize = 10;
                List<Student> allStudents = await studentServices.getAllStudent(page.Value, pageSize.Value);
                return Ok(allStudents);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{codeStudent}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getStudents([FromRoute] string codeStudent)
        {
            try
            {
                Student student = await studentServices.getStudentByCode(codeStudent);
                return Ok(student);
            }
            catch(StudentNotFoundException ex)
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> addStudent([FromBody] AddStudentModel model)
        {
            try
            {
                await userServices.checkLogin(model.Login);
                int id_user = await userServices.addUser(model.Login, model.Password);
                await studentServices.addStudent(model.NameStudent, model.GroupStudent, model.CodeStudent, id_user);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (InputInvalidException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (LoginExistsException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPatch("{codeStudent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> updateStudent([FromRoute] string codeStudent, [FromBody] UpdateStudent newStudent)
        {
            try
            {
                await studentServices.updateStudent(codeStudent, newStudent);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (InputInvalidException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (StudentNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPatch("changeRoom/{codeStudent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> changeRoom([FromRoute]string codeStudent, [FromBody] int id_room)
        {
            try
            {
                await studentServices.changeRoom(codeStudent, id_room);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (InputInvalidException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (StudentNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch(RoomNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch(StudentInRoomException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }
            catch (StudentNotLiveException ex)
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
