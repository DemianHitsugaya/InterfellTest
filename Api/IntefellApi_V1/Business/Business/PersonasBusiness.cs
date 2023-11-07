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
    public class PersonasBusiness : InterfellDBService<Persona>
    {
        public PersonasBusiness(SettingsHelper settings) : base(settings)
        {
        }

        public override bool Add(Persona entity)
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
                var dbEntity = BaseRepository.Query(x => x.IdPersona == entityID)
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

        public bool Delete(string numeroIdentificacion)
        {
            try
            {

                if(string.IsNullOrEmpty(numeroIdentificacion))
                    throw new ArgumentNullException("Don't exist records with this Id ");

                var dbEntity = BaseRepository.Query(x => x.Identificacion == numeroIdentificacion)
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

        public override Persona Get(int entityID)
        {
            try
            {
                if (entityID < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.IdPersona == entityID).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Persona Get(string numeroIdetificacion)
        {
            try
            {
                if (string.IsNullOrEmpty(numeroIdetificacion))
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.Identificacion== numeroIdetificacion).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IEnumerable<Persona> GetAll()
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

        public override bool Update(Persona entity)
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
