using Business.DBService;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Business.Business
{
    public class PersonAyudaBusiness : InterfellDBService<PersonaAyuda>
    {
        public PersonAyudaBusiness(SettingsHelper settings) : base(settings)
        {
        }

        public override bool Add(PersonaAyuda entity)
        {
            try
            {
                if (entity == null) { throw new ArgumentNullException(this.GetType().Name); }
                if (!Validate(entity.IdentificacionPersona, (int)entity.AyudaId, (int)entity.Año))
                    throw new ArgumentException("Invalid Data, Already exist a record with this data"); 
                BaseRepository.Insert(entity);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool AddRange(IEnumerable<PersonaAyuda> personaAyudas)
        {
            try
            {
                if (!personaAyudas.Any())
                    throw new ArgumentNullException(this.GetType().Name);

                BaseRepository.InsertRange(personaAyudas);
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


        public bool DeleteRange(IEnumerable<PersonaAyuda> entities)
        {
            try
            {
                if (entities.Any())
                {
                    var ArrayEntities = entities.ToArray();
                    for (int i = 0; i < ArrayEntities.Length; i++)
                    {
                        BaseRepository.Delete(ArrayEntities[i]);
                    }

                    return unitOfWork.SaveChanges() == ArrayEntities.Length;
                }
                else
                    return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool Delete(PersonaAyuda dbEntity)
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

        public override PersonaAyuda Get(int entityID)
        {
            return default(PersonaAyuda);
        }

        private bool Validate(string numeroIdentificacion, int ayudaId, int year)
        {
            try
            {
                if (string.IsNullOrEmpty(numeroIdentificacion) || ayudaId <= 0 || year <= 0)
                    throw new ArgumentNullException("Don't exist records with this data ");

                return !BaseRepository
                    .Query(x => x.IdentificacionPersona == numeroIdentificacion && x.AyudaId == ayudaId && x.Año == year)
                    .Select().Any();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IEnumerable<PersonaAyuda> GetAll()
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


        public IEnumerable<PersonaAyuda> GetByAyuda(int AyudaId)
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

        public IEnumerable<PersonaAyuda> GetByPersona(string numeroIdentificacion)
        {
            try
            {
                if (string.IsNullOrEmpty(numeroIdentificacion))
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.IdentificacionPersona == numeroIdentificacion).Select();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool Update(PersonaAyuda entity)
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
