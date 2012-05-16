using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PopApps.Monitor.Models.Domain;
using System.Linq.Expressions;
using PopApps.Monitor.Models;

namespace PopApps.Monitor.Models.DAL
{
    public class BaseRepository<T> where T: BaseEntity
    {
        public MyDbContext Context { get; set; }

        public BaseRepository(MyDbContext context)
        {
            this.Context = context;
        }
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
        protected DbSet<T> DbSet
        {
            get
            {
                return Context.Set<T>();
            }
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<T>();
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() :
                DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
                _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }
        public T Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }
        public virtual int Count()
        {

            return DbSet.Count();
        }
        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public IQueryable<T> All { get { return DbSet; } }


        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public T Find(Guid id)
        {
            return DbSet.Find(id);
        }


        public virtual T InsertOrUpdate(T entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                DbSet.Add(entity);
            }
            return entity;
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
            {
                DbSet.Remove(obj);
            }
        }

        public virtual void Delete(Guid id)
        {
            var entity = Find(id);
            DbSet.Remove(entity);
        }

        
    }
}