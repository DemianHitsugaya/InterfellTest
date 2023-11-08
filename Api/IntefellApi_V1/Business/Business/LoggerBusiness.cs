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
    public class LoggerBusiness : InterfellDBService<Logger>
    {
        public LoggerBusiness(SettingsHelper settings) : base(settings)
        {
        }

        public override bool Add(Logger entity)
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

        public override Logger Get(int entityID)
        {
            try
            {
                if (entityID < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.IdLogger == entityID).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IEnumerable<Logger> GetAll()
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

        public override bool Update(Logger entity)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Logger> GetLogs(DateOnly date)
        {
            try
            {
                return BaseRepository.Query(x => x.Fecha == date).Select();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
