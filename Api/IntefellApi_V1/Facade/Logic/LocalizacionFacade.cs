using AutoMapper;
using Business.Business;
using Entities.Entities;
using Facade.DTOs;
using Facade.Mappers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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


            _mapper = Mapping.configMapper();
        }


        #region PAIS       
        public PaisDTO GetPais(int paisId)
        {
            try
            {
                var pais = _paisBusiness.Get(paisId);
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
                    var arrayRegiones = regiones.Select(x => (int)x.IdRegion).ToArray();
                    if (regiones.Any())
                    {
                        for (int i = 0; i <= arrayRegiones.Length; i++)
                        {
                            var comunas = _comunaBusiness.GetByRegion(arrayRegiones[i]);
                            if (comunas.Any())
                            {
                                var comunasIds = comunas.Select(x => (int)x.IdComuna).ToArray();
                                for (int j = 0; j < comunasIds.Length; j++)
                                {
                                    var ayudas_comunas = _comunaAyudaBusiness.GetByComuna(comunasIds[j]);
                                    if (ayudas_comunas.Any())
                                        _comunaAyudaBusiness.DeleteRange(ayudas_comunas);
                                }
                                _comunaBusiness.DeleteRange(comunas);

                            }

                            var ayudas_region = _regionayudaBusiness.GetByRegion(arrayRegiones[i]);
                            if (ayudas_region.Any())
                                _regionayudaBusiness.DeleteRange(ayudas_region);
                        }
                        _regionBusiness.DeleteRange(regiones);
                    }
                }

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
                    var comunasIds = comunas.Select(x => (int)x.IdComuna).ToArray();
                    for (int j = 0; j < comunasIds.Length; j++)
                    {
                        var ayudas_comunas = _comunaAyudaBusiness.GetByComuna(comunasIds[j]);
                        if (ayudas_comunas.Any())
                            _comunaAyudaBusiness.DeleteRange(ayudas_comunas);
                    }
                    _comunaBusiness.DeleteRange(comunas);

                    var ayudas_region = _regionayudaBusiness.GetByRegion(regionId);
                    if (ayudas_region.Any())
                        _regionayudaBusiness.DeleteRange(ayudas_region);
                }


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
                var comunas= _comunaBusiness.GetAll();
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
