using Entities.Context;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Pattern.Infrastructure;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Business.DBService
{
    public abstract class InterfellDBService<T> where T : class, IObjectState
    {
        protected readonly InterfellContext interfellContext;
        protected readonly UnitOfWork unitOfWork;
        protected readonly Repository<T> BaseRepository;

        protected InterfellDBService(SettingsHelper settings)
        {
            if (string.IsNullOrEmpty(settings.Connection))
                interfellContext = new InterfellContext();
            else
            {
                if (interfellContext == null)
                {
                    string strconnection = Encoding.UTF8.GetString(Convert.FromBase64String(settings.Connection));
                    var option = new DbContextOptionsBuilder<InterfellContext>();
                    option.UseMySql(strconnection, ServerVersion.Parse("8.2.0-mysql"));

                    interfellContext = new InterfellContext(option.Options);
                }
            }

            unitOfWork = new UnitOfWork(interfellContext);
            BaseRepository = new Repository<T>(interfellContext, unitOfWork);

        }
        public abstract bool Add(T entity);
        public abstract bool Update(T entity);
        public abstract bool Delete(int entityID);
        public abstract IEnumerable<T> GetAll();
        public abstract T Get(int entityID);
    }
}
