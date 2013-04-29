using System;
using System.Data;
using System.Data.Entity.Validation;
using SharpLite.Domain.DataInterfaces;

namespace SharpLite.EntityFrameworkProvider
{
    public class DbContext : IDbContext
    {
        public DbContext(System.Data.Entity.DbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext may not be null");

            _dbContext = dbContext;
        }

        public virtual IDisposable BeginTransaction()
        {
            _transaction = _dbContext.Database.Connection.BeginTransaction();
            return _transaction;
        }

        /// <summary>
        /// This isn't specific to any one DAO and flushes everything that has been
        /// changed since the last commit.
        /// </summary>
        public virtual void CommitChanges()
        {
            try
            {
                OpenConnection();

                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public virtual void CommitTransaction()
        {
            if (_transaction != null)
                _transaction.Commit();
        }

        public virtual void RollbackTransaction()
        {
            if (_transaction != null)
                _transaction.Rollback();
        }

        private static IDbTransaction _transaction;
        private readonly System.Data.Entity.DbContext _dbContext;

        private void OpenConnection()
        {
            if (_dbContext != null && _dbContext.Database.Connection.State == ConnectionState.Closed) ;
            {
                _dbContext.Database.Connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (_dbContext != null && _dbContext.Database.Connection.State == ConnectionState.Open) ;
            {
                _dbContext.Database.Connection.Close();
            }
        }
    }
}
