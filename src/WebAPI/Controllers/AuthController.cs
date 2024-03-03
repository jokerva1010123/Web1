using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExtraModels;
using Models;
using Services;
using Exceptions;

namespace Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthController : Controller
    {
        private readonly UserServices userServices;

        public AuthController(UserServices userServices)
        {
            this.userServices = userServices;
        }
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            try 
            {
                User userlogin = await userServices.login(user.Login, user.Password);
                var role = userlogin.Level;
                return Ok(role);
            }
            catch (LoginNotFoundException ex) 
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch(IncorrectPasswordExcept ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [HttpPatch("changePassword/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> changePassword([FromBody] ChangePassModel newPass, [FromRoute] string username)
        {
            try
            {
                User? user = await userServices.getUserFromLogin(username);
                if (await userServices.changePassword(user.ID, user.Password, newPass.oldPass, newPass.newPass, newPass.newRepeatPass) != 1)
                    throw new DataBaseError();
                return Ok();
            }
            catch (InputInvalidException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (OldPasswordWrongException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (RepeateNewPassWordWrongException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (LoginNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}