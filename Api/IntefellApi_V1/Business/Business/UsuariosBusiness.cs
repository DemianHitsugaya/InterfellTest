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
    public class UsuariosBusiness : InterfellDBService<User>
    {
        public UsuariosBusiness(SettingsHelper settings) : base(settings)
        {
        }

        public override bool Add(User entity)
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

        public override User Get(int entityID)
        {
            try
            {
                if (entityID < 0)
                    throw new ArgumentNullException("Don't exist records with this Id ");

                return BaseRepository.Query(x => x.UserId == entityID).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User Get(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    throw new ArgumentNullException("Don't exist records with this Username ");

                return BaseRepository.Query(x => x.UserName == userName).Select().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public override bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
