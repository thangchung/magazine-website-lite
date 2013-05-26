using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using SharpLite.Domain;
using SharpLite.Domain.DataInterfaces;

namespace SharpLite.EntityFrameworkProvider
{
    public class Repository<T> : RepositoryWithTypedId<T, int>, IRepository<T> where T : class, IEntityWithTypedId<int>
    {
        public Repository(System.Data.Entity.DbContext dbContext) : base(dbContext) { }
    }

    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class, IEntityWithTypedId<TId>
    {
        public RepositoryWithTypedId(System.Data.Entity.DbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext may not be null");

            _dbContext = dbContext;
            Includes = new Collection<string>();
        }

        public virtual IDbContext DbContext
        {
            get
            {
                return new DbContext(_dbContext);
            }
        }

        public virtual T Get(TId id)
        {
            return SelectedSetWithIncludes.AsEnumerable().Single(entity => id.Equals(entity.Id));
        }

        public virtual IQueryable<T> GetAll()
        {
            return SelectedSetWithIncludes;
        }

        public virtual T SaveOrUpdate(T entity)
        {
            if (entity == null)
                return null;

            if (entity.IsTransient())
                _dbContext.Set<T>().Add(entity);

            return entity;
        }

        /// <summary>
        /// This deletes the object and commits the deletion immediately.  We don't want to delay deletion
        /// until a transaction commits, as it may throw a foreign key constraint exception which we could
        /// likely handle and inform the user about.  Accordingly, this tries to delete right away; if there
        /// is a foreign key constraint preventing the deletion, an exception will be thrown.
        /// </summary>
        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Include all sub table names, so when we executing the query, 
        /// it will be automatically get all data from sub-tables as well
        /// </summary>
        public IRepositoryWithTypedId<T, TId> Include(string subTableName)
        {
            Includes.Add(subTableName);

            return this;
        }

        private DbQuery<T> SelectedSetWithIncludes
        {
            get
            {
                DbQuery<T> query = _dbContext.Set<T>();

                foreach (var include in Includes)
                {
                    query = (DbQuery<T>)query.Include<T>(include);
                }

                return query;
            }
        }

        /// <summary>
        /// Query method will return IQueryable to Repository.
        /// </summary>
        /// <param name="where">
        /// Where condition on Entity.
        /// </param>
        /// <typeparam name="T">
        /// T Entity : conrete class
        /// </typeparam>
        /// <returns>
        /// IQueryable base on T Entity
        /// </returns>
        public IQueryable<T> Query<T>(Expression<Func<T, bool>> @where)
        {
            return ((IQueryable<T>)this._dbContext.Set(typeof(T))).Where(@where);
        }

        private readonly System.Data.Entity.DbContext _dbContext;
        private Collection<string> Includes { get; set; }
    }
}