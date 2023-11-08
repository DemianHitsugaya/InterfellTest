using AutoMapper;
using Business.Business;
using Entities.Entities;
using Facade.DTOs;
using Facade.Mappers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Facade.Logic
{
    public class LocalizacionFacade
    {
        private PaisBusiness _paisBusiness;
        private RegionBusiness _regionBusiness;
        private ComunaBusiness _comunaBusiness;
        private RegionAyudasBusiness _regionayudaBusiness;
        private AyudasComunaBusiness _comunaAyudaBusiness;
        private LoggerFacade _logger;

        private IMapper _mapper;

        public LocalizacionFacade(SettingsHelper settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (string.IsNullOrEmpty(settings.Connection))
                throw new ArgumentNullException(nameof(settings.Connection));

            _paisBusiness = new PaisBusiness(settings);
            _regionBusiness = new RegionBusiness(settings);
            _comunaBusiness = new ComunaBusiness(settings);
            _regionayudaBusiness = new RegionAyudasBusiness(settings);
            _comunaAyudaBusiness = new AyudasComunaBusiness(settings);

            _logger = new LoggerFacade(settings);


            _mapper = Mapping.configMapper();
        }


        #region PAIS       
        public PaisDTO GetPais(int paisId)
        {
            try
            {
                var pais = _paisBusiness.Get(paisId);
                _logger.Log(Entidades.Pais, Acciones.ReadOne, JsonConvert.SerializeObject(pais));
                return _mapper.Map<PaisDTO>(pais);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PaisDTO GetPais(string nombrePais)
        {
            try
            {
                var pais = _paisBusiness.Get(nombrePais);
                _logger.Log(Entidades.Pais, Acciones.ReadOne, JsonConvert.SerializeObject(pais));
                return _mapper.Map<PaisDTO>(pais);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<PaisDTO> GetAllPaises()
        {
            try
            {
                var paises = _paisBusiness.GetAll();
                _logger.Log(Entidades.Pais, Acciones.ReadAll, JsonConvert.SerializeObject(paises));
                return _mapper.Map<IEnumerable<Pais>, IEnumerable<PaisDTO>>(paises);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool AddPais(PaisDTO pais)
        {
            try
            {
                _logger.Log(Entidades.Pais, Acciones.Create, string.Empty, JsonConvert.SerializeObject(pais));
                return _paisBusiness.Add(_mapper.Map<Pais>(pais));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdatePais(PaisDTO pais)
        {
            try
            {
                var oldEntity = _paisBusiness.Get(pais.NomPais);
                var newEntity = _mapper.Map<PaisDTO, Pais>(pais, opt =>
                {
                    opt.AfterMap((src, dest) => dest.IdPais = oldEntity.IdPais);
                });
                _logger.Log(Entidades.Pais, Acciones.Update, JsonConvert.SerializeObject(oldEntity), JsonConvert.SerializeObject(newEntity));
                return _paisBusiness.Update(newEntity);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeletePais(int paisID, bool enCascada = false)
        {
            try
            {

                if (enCascada)
                {
                    var regiones = _regionBusiness.GetByPais(paisID);                 
                    if (regiones.Any())
                    {
                        var arrayRegiones = regiones.Select(x => (int)x.IdRegion).ToList();
                        var comunas  = _comunaBusiness.GetAll().Where(x=> arrayRegiones.Contains((int)x.CodRegion)).ToList();                       

                        if (comunas.Any())
                        {
                            var comunasIds = comunas.Select(x => x.IdComuna).ToList();
                            var ayudas_comuna = _comunaAyudaBusiness.GetAll().Where(x=> comunasIds.Contains(x.ComunaId)).ToList();
                            
                            if(!_comunaAyudaBusiness.DeleteRange(ayudas_comuna))
                                return false;

                            if (!_comunaBusiness.DeleteRange(comunas))
                                return false;
                        }


                        var regionAyuda = _regionayudaBusiness.GetAll().Where(x => arrayRegiones.Contains((int)x.RegionId)).ToList();
                        if (!_regionayudaBusiness.DeleteRange(regionAyuda))
                            return false;                        

                        if (!_regionBusiness.DeleteRange(regiones))
                            return false;
                    }
                }
                _logger.Log(Entidades.Pais, Acciones.Delete, JsonConvert.SerializeObject(new { idPais = paisID }));
                return _paisBusiness.Delete(paisID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region Regiones

        public bool AddRegion(RegionDTO regionDTO)
        {
            try
            {
                _logger.Log(Entidades.Region, Acciones.Create, string.Empty, JsonConvert.SerializeObject(regionDTO));
                return _regionBusiness.Add(_mapper.Map<Region>(regionDTO));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateRegion(RegionDTO region)
        {
            try
            {
                var oldEntity = _regionBusiness.Get(region.NomRegion);
                var newEntity = _mapper.Map<RegionDTO, Region>(region, opt =>
                {
                    opt.AfterMap((src, dest) => dest.IdRegion = oldEntity.IdRegion);
                });
                _logger.Log(Entidades.Region, Acciones.Update, JsonConvert.SerializeObject(oldEntity), JsonConvert.SerializeObject(newEntity));
                return _regionBusiness.Update(newEntity);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteRegion(int regionId, bool enCascada = false)
        {
            try
            {

                if (enCascada)
                {
                    var comunas = _comunaBusiness.GetByRegion(regionId);               

                    if (comunas.Any())
                    {
                        var comunasIds = comunas.Select(x => x.IdComuna).ToList();
                        var ayudas_comuna = _comunaAyudaBusiness.GetAll().Where(x => comunasIds.Contains(x.ComunaId)).ToList();
                        _comunaAyudaBusiness.DeleteRange(ayudas_comuna);

                        if (!_comunaBusiness.DeleteRange(comunas))
                            return false;
                    }


                    var regionAyuda = _regionayudaBusiness.GetByRegion(regionId);
                    if (!_regionayudaBusiness.DeleteRange(regionAyuda))
                        return false;
                }

                _logger.Log(Entidades.Region, Acciones.Delete, JsonConvert.SerializeObject(new { idRegion = regionId }));
                return _regionBusiness.Delete(regionId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RegionDTO GetRegion(int regionId)
        {
            try
            {
                var region = _regionBusiness.Get(regionId);
                _logger.Log(Entidades.Region, Acciones.ReadOne, JsonConvert.SerializeObject(region));
                return _mapper.Map<RegionDTO>(region);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<RegionDTO> GetAllRegiones()
        {
            try
            {
                var regiones = _regionBusiness.GetAll();
                _logger.Log(Entidades.Region, Acciones.ReadAll, JsonConvert.SerializeObject(regiones));
                return _mapper.Map<IEnumerable<Region>, IEnumerable<RegionDTO>>(regiones);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public RegionDTO GetRegion(string nombreRegion)
        {
            try
            {
                var region = _regionBusiness.Get(nombreRegion);
                _logger.Log(Entidades.Region, Acciones.ReadOne, JsonConvert.SerializeObject(region));
                return _mapper.Map<RegionDTO>(region);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Comunas
        public bool AddComuna(ComunaDTO comunaDTO)
        {
            try
            {
                _logger.Log(Entidades.Region, Acciones.Create, string.Empty, JsonConvert.SerializeObject(comunaDTO));
                return _comunaBusiness.Add(_mapper.Map<Comuna>(comunaDTO));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateComuna(ComunaDTO comunaDTO)
        {
            try
            {
                var oldEntity = _comunaBusiness.Get(comunaDTO.NomComuna);
                var newEntity = _mapper.Map<ComunaDTO, Comuna>(comunaDTO, opt =>
                {
                    opt.AfterMap((src, dest) => dest.IdComuna = oldEntity.IdComuna);
                });
                _logger.Log(Entidades.Comuna, Acciones.Update, JsonConvert.SerializeObject(oldEntity), JsonConvert.SerializeObject(newEntity));
                return _comunaBusiness.Update(newEntity);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteComuna(int comunaId, bool enCascada = false)
        {
            try
            {

                if (enCascada)
                {
                    var ayudas_comunas = _comunaAyudaBusiness.GetByComuna(comunaId);
                    if (ayudas_comunas.Any())
                        _comunaAyudaBusiness.DeleteRange(ayudas_comunas);

                }

                _logger.Log(Entidades.Comuna, Acciones.Delete, JsonConvert.SerializeObject(new { ComunaId = comunaId }));
                return _comunaBusiness.Delete(comunaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ComunaDTO GetComuna(int comunaId)
        {
            try
            {
                var comuna = _comunaBusiness.Get(comunaId);
                _logger.Log(Entidades.Comuna, Acciones.ReadOne, JsonConvert.SerializeObject(comuna));
                return _mapper.Map<ComunaDTO>(comuna);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ComunaDTO GetComuna(string nombreComuna)
        {
            try
            {
                var comuna = _comunaBusiness.Get(nombreComuna);
                _logger.Log(Entidades.Comuna, Acciones.ReadOne, JsonConvert.SerializeObject(comuna));
                return _mapper.Map<ComunaDTO>(comuna);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<ComunaDTO> GetComunas()
        {
            try
            {
                var comunas = _comunaBusiness.GetAll();
                _logger.Log(Entidades.Comuna, Acciones.ReadAll, JsonConvert.SerializeObject(comunas));
                return _mapper.Map<IEnumerable<Comuna>, IEnumerable<ComunaDTO>>(comunas);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ComunaDTO> GetComunas(int RegionId)
        {
            try
            {
                var comunas = _comunaBusiness.GetByRegion(RegionId);
                _logger.Log(Entidades.Comuna, Acciones.ReadAll, JsonConvert.SerializeObject(comunas));
                return _mapper.Map<IEnumerable<Comuna>, IEnumerable<ComunaDTO>>(comunas);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
