using Facade.DTOs;
using Facade.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repository;
using Utilities;

namespace Interfell.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserFacade _userFacade;
        private LoggerFacade _loggerFacade;
        public UserController(IOptions<SettingsHelper> settings, IOptions<Jwt> jwt)
        {
            _userFacade = new UserFacade(settings.Value, jwt.Value);
            _loggerFacade = new LoggerFacade(settings.Value);
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            try
            {
                var token = _userFacade.Login(login);
                if (login == null) { return BadRequest("Entity is null"); }
                if (!token.UserValid) return Unauthorized("Usuario y/o Password Incorrectos");

                return Ok(token.Token);
            }
            catch (Exception ex)
            {

                return BadRequest(ex); ;
            }
        }

        [HttpPost("AddUser")]
        [Authorize]
        public IActionResult CrearUsuario([FromBody] UserDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_userFacade.AddUser(entity))
                    return UnprocessableEntity("Created Failed");

                return Ok("Usuario Creado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetLogs")]
        [Authorize]
        public IActionResult GetLogs([FromQuery] DateTime datefilter)
        {
            try
            {
                return Ok(_loggerFacade.GetLogs(datefilter));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
