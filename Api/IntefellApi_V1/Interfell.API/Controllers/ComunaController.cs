using Facade.DTOs;
using Facade.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Utilities;

namespace Interfell.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComunaController : ControllerBase
    {
        private LocalizacionFacade _localizacion;

        public ComunaController(IOptions<SettingsHelper> settings)
        {
            _localizacion = new LocalizacionFacade(settings.Value);
        }

        [HttpGet("GetComunas")]
        public IActionResult GetComunas()
        {
            try
            {
                return Ok(_localizacion.GetComunas());
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("GetComunaById")]
        public IActionResult GetComuna([FromQuery] int paisId)
        {
            try
            {
                return Ok(_localizacion.GetComuna(paisId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetComunaByName")]
        public IActionResult GetComuna([FromQuery] string nombrePais)
        {
            try
            {
                return Ok(_localizacion.GetComuna(nombrePais));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CreateComuna")]
        [Authorize]
        public IActionResult CreateComuna([FromBody] ComunaDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_localizacion.AddComuna(entity))
                    return UnprocessableEntity("Created Failed");

                return Ok("Region Creado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdateComuna")]
        [Authorize]
        public IActionResult UpdateComuna([FromBody]ComunaDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_localizacion.UpdateComuna(entity))
                    return UnprocessableEntity("UPdate Failed");

                return Ok("Comuna Modificada Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteComuna([FromQuery] int entityId, [FromQuery] bool inCascada = false)
        {
            try
            {
                if (entityId < 0)
                    return BadRequest("Invalid Parameter");
                if (!_localizacion.DeleteRegion(entityId, inCascada))
                    return UnprocessableEntity("Action failed");
                return Ok("Region Deleted Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
