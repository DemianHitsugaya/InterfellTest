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
    public class RegionController : ControllerBase
    {
        private LocalizacionFacade _localizacion;

        public RegionController(IOptions<SettingsHelper> settings)
        {
            _localizacion = new LocalizacionFacade(settings.Value);
        }

        [HttpGet("GetRegiones")]
        public IActionResult GetRegiones()
        {
            try
            {
                return Ok(_localizacion.GetAllRegiones());
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("GetRegionById")]
        public IActionResult GetRegion([FromQuery] int paisId)
        {
            try
            {
                return Ok(_localizacion.GetRegion(paisId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetRegionByName")]
        public IActionResult GetRegion([FromQuery] string nombrePais)
        {
            try
            {
                return Ok(_localizacion.GetRegion(nombrePais));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CreateRegion")]
        [Authorize]
        public IActionResult CreateRegion([FromBody] RegionDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_localizacion.AddRegion(entity))
                    return UnprocessableEntity("Created Failed");

                return Ok("Region Creado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdateRegion")]
        [Authorize]
        public IActionResult UpdateRegion([FromBody] RegionDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_localizacion.UpdateRegion(entity))
                    return UnprocessableEntity("UPdate Failed");

                return Ok("Region Modificado Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteRegion([FromQuery] int entityId, [FromQuery] bool inCascada = false)
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
