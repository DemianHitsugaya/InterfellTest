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
    public class AyudasBusiness : InterfellDBService<Ayuda>
    {
        public AyudasBusiness(SettingsHelper settings) : base(settings)
        {
        }

        public int CreateLastId(Ayuda entity)
        {
            try
            {
                Add(entity);
                return (int)entity.IdAyuda;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Add(Ayuda entity)
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
                var dbEntity = BaseRepository.Query(x => x.IdAyuda == entityID)
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

        public override Ayuda Get(int entityID)
        {
            try
            {
                if (entityID < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.IdAyuda == entityID).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IEnumerable<Ayuda> GetAll()
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

        public override bool Update(Ayuda entity)
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
