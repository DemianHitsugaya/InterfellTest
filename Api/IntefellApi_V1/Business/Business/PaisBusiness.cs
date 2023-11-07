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
    public class PaisBusiness : InterfellDBService<Pais>
    {
        public PaisBusiness(SettingsHelper settings) : base(settings)
        {
        }

        public override bool Add(Pais entity)
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
                var dbEntity = BaseRepository.Query(x => x.IdPais == entityID)
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

        public override Pais Get(int entityID)
        {
            try
            {
                if (entityID <0 )
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.IdPais == entityID).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Pais Get(string paisNombre)
        {
            try
            {
                if (string.IsNullOrEmpty(paisNombre))
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.NomPais.ToLower() == paisNombre.ToLower()).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IEnumerable<Pais> GetAll()
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

        public override bool Update(Pais entity)
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
