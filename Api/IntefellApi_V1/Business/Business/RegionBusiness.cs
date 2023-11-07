using Business.DBService;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Business.Business
{
    public class RegionBusiness : InterfellDBService<Region>
    {
        public RegionBusiness(SettingsHelper settings) : base(settings)
        {
        }

        public override bool Add(Region entity)
        {
            try
            {
                if (entity == null) { throw new ArgumentNullException(this.GetType().Name); }
                BaseRepository.Insert(entity);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Delete(int entityID)
        {
            try
            {
                var dbEntity = BaseRepository.Query(x => x.IdRegion == entityID)
                .Select()
                .FirstOrDefault();

                if (dbEntity == null)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                BaseRepository.Delete(dbEntity);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteRange(IEnumerable<Region> entities)
        {
            try
            {
                var ArrayEntities = entities.ToArray();
                for (int i = 0; i < ArrayEntities.Length; i++)
                {
                    BaseRepository.Delete(ArrayEntities[i]);
                }

                return unitOfWork.SaveChanges() == ArrayEntities.Length;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override Region Get(int entityID)
        {
            try
            {
                if (entityID <0 )
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.IdRegion == entityID).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Region Get(string nombreRegion)
        {
            try
            {
                if (string.IsNullOrEmpty(nombreRegion))
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.NomRegion.ToLower() == nombreRegion.ToLower()).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IEnumerable<Region> GetAll()
        {
            try
            {
                return BaseRepository.Query().Select();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Update(Region entity)
        {
            try
            {
                if (entity == null) { throw new ArgumentNullException(this.GetType().Name); }
                BaseRepository.Update(entity);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Region> GetByPais(int paisId)
        {
            try
            {
                if (paisId < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.PaisId== paisId).Select();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
