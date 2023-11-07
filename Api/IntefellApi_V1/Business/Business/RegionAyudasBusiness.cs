using Business.DBService;
using Entities.Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Business.Business
{
    public class RegionAyudasBusiness : InterfellDBService<AyudasRegion>
    {
        public RegionAyudasBusiness(SettingsHelper settings) : base(settings)
        {
        }

        public override bool Add(AyudasRegion entity)
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
            throw new NotImplementedException();
        }

        public bool Delete(AyudasRegion dbEntity)
        {
            try
            {
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

        public bool DeleteRange(IEnumerable<AyudasRegion> entities)
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

        public override AyudasRegion Get(int entityID)
        {
            return default(AyudasRegion);
        }

        public AyudasRegion Get(int AyudaId, int RegionId)
        {
            try
            {
                if (AyudaId < 0 && RegionId < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.AyudaId == AyudaId && x.RegionId == RegionId).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IEnumerable<AyudasRegion> GetAll()
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

        public IEnumerable<AyudasRegion> GetByAyuda(int AyudaId)
        {
            try
            {
                if (AyudaId < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.AyudaId == AyudaId).Select();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AyudasRegion> GetByRegion(int RegionId)
        {
            try
            {
                if (RegionId < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.RegionId == RegionId).Select();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Update(AyudasRegion entity)
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
    }
}
