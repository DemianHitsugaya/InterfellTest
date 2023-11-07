using AutoMapper;
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
    public class PaisController : ControllerBase
    {
        private LocalizacionFacade _localizacion;

        public PaisController(IOptions<SettingsHelper> settings)
        {
            _localizacion = new LocalizacionFacade(settings.Value);
        }

        [HttpGet("GetPaises")]
        public IActionResult GetPaises()
        {
            try
            {
                return Ok(_localizacion.GetAllPaises());
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("GetPaisById")]
        public IActionResult GetPais([FromQuery] int paisId)
        {
            try
            {
                return Ok(_localizacion.GetPais(paisId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetPaisByName")]
        public IActionResult GetPais([FromQuery] string nombrePais)
        {
            try
            {
                return Ok(_localizacion.GetPais(nombrePais));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CreatePais")]
        [Authorize]
        public IActionResult CreatePais([FromBody] PaisDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_localizacion.AddPais(entity))
                    return UnprocessableEntity("Created Failed");

                return Ok("Pais Creado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdatePais")]
        [Authorize]
        public IActionResult UpdatePais([FromBody] PaisDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_localizacion.UpdatePais(entity))
                    return UnprocessableEntity("UPdate Failed");

                return Ok("Pais Modificado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeletePais([FromQuery] int entityId, [FromQuery] bool inCascada = false)
        {
            try
            {
                if (entityId < 0)
                    return BadRequest("Invalid Parameter");
                if (!_localizacion.DeletePais(entityId, inCascada))
                    return UnprocessableEntity("Action failed");
                return Ok("Pais Deleted Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
