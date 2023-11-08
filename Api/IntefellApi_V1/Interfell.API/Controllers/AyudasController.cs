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
    public class AyudasController : ControllerBase
    {
        AyudasFacade _ayudasFacade;
        public AyudasController(IOptions<SettingsHelper> settings)
        {
            _ayudasFacade = new AyudasFacade(settings.Value);
        }

        [HttpGet("GetAyudas")]
        [Authorize]
        public IActionResult GetAyudas([FromBody] AyudaRequestDTO ayudaRequest)
        {
            try
            {
                if (ayudaRequest == null)
                    return BadRequest("Entity is null");
                return Ok(_ayudasFacade.GetAyudaInfo(ayudaRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteAyudas([FromQuery] int entityId)
        {
            try
            {
                if (entityId < 0)
                    return BadRequest("Invalid Parameter");
                if (!_ayudasFacade.DeleteAyuda(entityId))
                    return UnprocessableEntity("Action failed");
                return Ok("Ayuda Deleted Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("AsignarAyudaComuna")]
        [Authorize]
        public IActionResult AsignarAyudaComuna([FromBody] AyudaComunaDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_ayudasFacade.AssignarAyudaComuna(entity))
                    return UnprocessableEntity("Created Failed");

                return Ok("Ayuda Asignada Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("AsignarAyudaRegion")]
        [Authorize]
        public IActionResult AsignarAyudaRegion([FromBody] AyudaRegionDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_ayudasFacade.AssignAyudaRegion(entity))
                    return UnprocessableEntity("Created Failed");

                return Ok("Ayuda Asignada Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("AsignarAyudaPersona")]
        [Authorize]
        public IActionResult AsignarAyudaPersona([FromBody] AyudaPersonaAddDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_ayudasFacade.AsignAyudaToPersona(entity))
                    return UnprocessableEntity("Created Failed");

                return Ok("Ayuda Asignada Correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdateAyuda")]
        [Authorize]
        public IActionResult UpdateAyuda(AyudaDTO entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Entity is null");
                if (!_ayudasFacade.UpdateAyuda(entity))
                    return UnprocessableEntity("UPdate Failed");

                return Ok("Ayuda Modificada Correctamente");
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("GetAyudaByPersona")]
        [Authorize]
        public IActionResult GetPais([FromQuery] string numeroDocumento)
        {
            try
            {
                return Ok(_ayudasFacade.GetAyudasByPersona(numeroDocumento));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
