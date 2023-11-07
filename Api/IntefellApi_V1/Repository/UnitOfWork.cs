using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace Repository
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields

        private IDataContextAsync _dataContext;
        private bool _disposed;
        private DbTransaction _transaction;
        private Dictionary<string, dynamic> _repositories;

        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(IDataContextAsync dataContext)
        {
            _dataContext = dataContext;
            _repositories = new Dictionary<string, dynamic>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    _dataContext.Dispose();
                }
                catch (ObjectDisposedException)
                {
                }

                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }
            }
            _disposed = true;
        }

        #endregion Constuctor/Dispose

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState
        {
            return RepositoryAsync<TEntity>();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dataContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState
        {

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext, this));

            return _repositories[type];
        }

        #region Unit of Work Transactions
        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _dataContext.SyncObjectsStatePostCommit();
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
        }

        #endregion
    }
}
