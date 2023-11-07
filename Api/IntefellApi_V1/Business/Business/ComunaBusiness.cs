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
    public class ComunaBusiness : InterfellDBService<Comuna>
    {
        public ComunaBusiness(SettingsHelper settings) : base(settings)
        {
        }

        public override bool Add(Comuna entity)
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
                var dbEntity = BaseRepository.Query(x => x.IdComuna == entityID)
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

        public bool DeleteRange(IEnumerable<Comuna> entities)
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

        public override Comuna Get(int entityID)
        {
            try
            {
                if (entityID < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.IdComuna == entityID).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Comuna Get(string nombreComuna)
        {
            try
            {
                if (string.IsNullOrEmpty(nombreComuna))
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.NomComuna== nombreComuna).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IEnumerable<Comuna> GetAll()
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

        public override bool Update(Comuna entity)
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

        public IEnumerable<Comuna> GetByRegion (int regionID)
        {
            try
            {
                if (regionID < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.CodRegion == regionID).Select();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
