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
    public class AyudasComunaBusiness : InterfellDBService<AyudasComuna>
    {
        public AyudasComunaBusiness(SettingsHelper settings) : base(settings)
        {
        }

        public override bool Add(AyudasComuna entity)
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

        public bool AddRange(IEnumerable<AyudasComuna> personaAyudas)
        {
            try
            {
                if (!personaAyudas.Any())
                    throw new ArgumentNullException(this.GetType().Name);

                BaseRepository.InsertRange(personaAyudas);
                return unitOfWork.SaveChanges() == personaAyudas.Count();

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

        public bool Delete(AyudasComuna dbEntity)
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

        public bool DeleteRange(IEnumerable<AyudasComuna> entities)
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

        public override AyudasComuna Get(int entityID)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<AyudasComuna> GetAll()
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

        public IEnumerable<AyudasComuna> GetByAyuda(int AyudaId)
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

        public IEnumerable<AyudasComuna> GetByComuna(int ComunaId)
        {
            try
            {
                if (ComunaId < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.ComunaId == ComunaId).Select();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Update(AyudasComuna entity)
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
