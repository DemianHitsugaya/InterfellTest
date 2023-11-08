using AutoMapper;
using Business.Business;
using Entities.Entities;
using Facade.DTOs;
using Facade.Mappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Facade.Logic
{
    public class AyudasFacade
    {
        private LoggerFacade _logger;
        private IMapper _mapper;

        private AyudasBusiness _ayudaBusiness;
        private AyudasComunaBusiness _ayudaComunaBusiness;
        private LocalizacionFacade _localizacionFacade;
        private PersonasBusiness _personasBusiness;
        private PersonAyudaBusiness _personAyudaBusiness;
        private RegionAyudasBusiness _regionAyudaBusiness;


        public AyudasFacade(SettingsHelper settings)
        {

            _localizacionFacade = new LocalizacionFacade(settings);
            _logger = new LoggerFacade(settings);
            _mapper = Mapping.configMapper();

            _ayudaBusiness = new AyudasBusiness(settings);
            _ayudaComunaBusiness = new AyudasComunaBusiness(settings);
            _personasBusiness = new PersonasBusiness(settings);
            _personAyudaBusiness = new PersonAyudaBusiness(settings);
            _regionAyudaBusiness = new RegionAyudasBusiness(settings);
        }


        public bool AddAyuda(AyudaDTO ayuda)
        {
            try
            {
                var lastId = _ayudaBusiness.CreateLastId(_mapper.Map<Ayuda>(ayuda));
                ayuda.IdAyuda = lastId;
                _logger.Log(Entidades.Ayudas, Acciones.Create, string.Empty, JsonConvert.SerializeObject(ayuda));

                if (lastId < 0)
                {
                    return false;
                }
                else
                {
                    if (ayuda.EsRegional == true && ayuda.RegionID > 0)
                    {
                        var regionAyuda = new AyudaRegionDTO { AyudaId = (uint)lastId, RegionId = (uint)ayuda.RegionID };
                        AssignAyudaRegion(regionAyuda);
                    }
                    else if (ayuda.EsComunal == true && ayuda.ComunaId > 0)
                    {
                        var ayudas_comunas = new AyudasComuna { ComunaId = (uint)ayuda.ComunaId, AyudaId = (uint)lastId };
                        _ayudaComunaBusiness.Add(ayudas_comunas);
                        _logger.Log(Entidades.Ayudas_Comuna, Acciones.Create, string.Empty, JsonConvert.SerializeObject(ayudas_comunas));
                    }

                    return true;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAyuda (AyudaDTO ayuda)
        {
            try
            {
                var oldEntity = _ayudaBusiness.Get((int)ayuda.IdAyuda);
                var newEntity = _mapper.Map<AyudaDTO, Ayuda>(ayuda, opt =>
                {
                    opt.AfterMap((src, dest) => dest.IdAyuda = oldEntity.IdAyuda);
                });
                _logger.Log(Entidades.Ayudas, Acciones.Update, JsonConvert.SerializeObject(oldEntity), JsonConvert.SerializeObject(newEntity));
                bool ayuda_update =  _ayudaBusiness.Update(newEntity);
                if(oldEntity.EsRegional != newEntity.EsRegional)
                {
                    if(newEntity.EsRegional == true && ayuda.RegionID > 0)
                    {
                        var regionAyuda = new AyudaRegionDTO { AyudaId = (uint)newEntity.IdAyuda, RegionId = (uint)ayuda.RegionID };
                        if(!AssignAyudaRegion(regionAyuda))
                            return false;
                    }

                }

                if (oldEntity.EsComunal != newEntity.EsComunal)
                {
                    if (newEntity.EsComunal == true && ayuda.ComunaId > 0)
                    {
                        var ayudas_comunas = new AyudasComuna { ComunaId = (uint)ayuda.ComunaId, AyudaId = (uint)newEntity.IdAyuda };
                        if(!_ayudaComunaBusiness.Add(ayudas_comunas))
                            return false;
                        _logger.Log(Entidades.Ayudas_Comuna, Acciones.Create, string.Empty, JsonConvert.SerializeObject(ayudas_comunas));
                    }

                }


                return ayuda_update;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AsignAyudaToPersona(AyudaPersonaAddDTO entity)
        {
            try
            {
                var ayudaPersona = _mapper.Map<PersonaAyuda>(entity);
                ayudaPersona.Año = (uint?)DateTime.Now.Year;
                _logger.Log(Entidades.Persona_Ayudas, Acciones.Create, string.Empty, JsonConvert.SerializeObject(entity));
                return _personAyudaBusiness.Add(ayudaPersona);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AssignAyudaRegion(AyudaRegionDTO entity)
        {

            try
            {
                var regionAyudaDb = _mapper.Map<AyudasRegion>(entity);
                _logger.Log(Entidades.Ayudas_Region, Acciones.Create, string.Empty, JsonConvert.SerializeObject(regionAyudaDb));
                _regionAyudaBusiness.Add(regionAyudaDb);


                var comunas = _localizacionFacade.GetComunas((int)entity.RegionId).Select(x => x.IdComuna).ToList();

                List<AyudasComuna> ayudasComunas = new List<AyudasComuna>();
                for (int i = 0; i < comunas.Count; i++)
                {
                    ayudasComunas.Add(new AyudasComuna { AyudaId = (uint)entity.AyudaId, ComunaId = (uint)comunas[i] });
                }
                var result = _ayudaComunaBusiness.AddRange(ayudasComunas);
                _logger.Log(Entidades.Ayudas_Comuna, Acciones.CreateRange, string.Empty, JsonConvert.SerializeObject(ayudasComunas));

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AssignarAyudaComuna(AyudaComunaDTO ayudaComuna)
        {
            try
            {
                _logger.Log(Entidades.Ayudas_Comuna, Acciones.Create, string.Empty, JsonConvert.SerializeObject(ayudaComuna));
                return _ayudaComunaBusiness.Add(_mapper.Map<AyudasComuna>(ayudaComuna));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AyudasResponseDTO GetAyudaInfo(AyudaRequestDTO ayudaRequest)
        {
            try
            {
                if (ayudaRequest == null)
                    throw new ArgumentNullException(this.GetType().Name);

                if (ayudaRequest.IdAyuda <= 0)
                    throw new ArgumentException(this.GetType().Name, new Exception("Invalid Id"));

                var ayudaResponse = new AyudasResponseDTO();

                var ayudaDb = _ayudaBusiness.Get(ayudaRequest.IdAyuda);
                ayudaResponse.Ayuda = _mapper.Map<AyudaDTO>(ayudaDb);

                if (ayudaRequest.LoadRegiones)
                {
                    var  ayudaRegiones = _regionAyudaBusiness.GetByAyuda(ayudaRequest.IdAyuda).Select(x=>(int)x.RegionId).ToList();
                    var regiones = _localizacionFacade.GetAllRegiones().Where(x =>ayudaRegiones.Contains((int)x.IdRegion)).ToList();

                    ayudaResponse.Regiones = regiones;

                }

                if (ayudaRequest.LoadComunas)
                {
                    var ayudasComunas = _ayudaComunaBusiness.GetByAyuda(ayudaRequest.IdAyuda).Select(x => (int)x.ComunaId).ToList();
                    var comunas = _localizacionFacade.GetComunas().Where(x => ayudasComunas.Contains((int)x.IdComuna)).ToList();

                    ayudaResponse.Comunas = comunas;

                }

                if (ayudaRequest.LoadPersonas)
                {
                    var ayudaPersonas = _personAyudaBusiness.GetByAyuda(ayudaRequest.IdAyuda).Select(x => x.IdentificacionPersona).ToList();
                    var personas = _personasBusiness.GetAll().Where(x => ayudaPersonas.Contains(x.Identificacion)).ToList();

                    ayudaResponse.Personas = _mapper.Map<IEnumerable<Persona>, IEnumerable<PersonasDTO>>(personas);

                }


                return ayudaResponse;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool DeleteAyuda(int AyudaId)
        {
            try
            {
                if(AyudaId<= 0)
                    throw new ArgumentNullException(nameof(AyudaId));

                if (_personAyudaBusiness.DeleteRange(_personAyudaBusiness.GetByAyuda(AyudaId)))
                    return false;

                if(!_ayudaComunaBusiness.DeleteRange(_ayudaComunaBusiness.GetByAyuda(AyudaId)))
                    return false;

                if (!_regionAyudaBusiness.DeleteRange(_regionAyudaBusiness.GetByAyuda(AyudaId)))
                    return false;


                return _ayudaBusiness.Delete(AyudaId);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public PersonaAyudasResponseDTO GetAyudasByPersona(string numeroIdentificacion)
        {
            try
            {

                PersonaAyudasResponseDTO responseDTO = new PersonaAyudasResponseDTO();
                if (string.IsNullOrEmpty(numeroIdentificacion))
                    throw new ArgumentNullException(this.GetType().Name, new Exception("Invalid Data"));


                responseDTO.Persona = _mapper.Map<PersonasDTO>(_personasBusiness.Get(numeroIdentificacion));
                
                var ayuda_persona = _personAyudaBusiness.GetByPersona(numeroIdentificacion);
                if (ayuda_persona.Any())
                {
                    var ayudasId = ayuda_persona.Select(x=>x.AyudaId).ToList();

                    var ayudas = _ayudaBusiness.GetAll().Where(x => ayudasId.Contains(x.IdAyuda)).ToList();
                    responseDTO.Ayudas = _mapper.Map<IEnumerable<Ayuda>, IEnumerable<AyudaDTO>>(ayudas);
                }
                
                return responseDTO;

            }
            catch (Exception)
            {

                throw;
            }
        }






    }
}
