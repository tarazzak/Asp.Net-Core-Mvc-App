using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using School.Services.Interfaces;

namespace School.Services.Implementations
{
    public class SchoolService<T> : ISchoolService<T> where T : class
    {
        protected SchoolContext Context = new SchoolContext(new DbContextOptions<SchoolContext>());
        protected DbSet<T> Entities => Context.Set<T>();

        public virtual IQueryable<T> GetAll(bool asNoTracking = true, bool includeChildren = false)
        {
            return asNoTracking ? Entities.AsNoTracking() : Entities;
        }

        public virtual IEnumerable<T> GetAllEnumerable(bool asNoTracking = true, bool includeChildren = false)
        {
            return asNoTracking ? Entities.AsNoTracking().AsEnumerable() : Entities.AsEnumerable();
        }

        public virtual IQueryable<T> FindByCriteria(Expression<Func<T, bool>> expression, bool asNoTracking = true, bool includeChildren = false)
        {
            return asNoTracking ? Entities.Where(expression).AsNoTracking() : Entities.Where(expression);
        }

        public virtual T Update(T entity)
        {
            Entities.Update(entity);
            Context.SaveChanges();
            return Context.Entry(entity).Entity;
        }

        public virtual T Remove(T entity)
        {
            Entities.Remove(entity);
            Context.SaveChanges();
            Context.Entry(entity).State = EntityState.Modified;
            return Context.Entry(entity).Entity;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Entities.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<T> FindFirstByCriteriaAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true)
        {
            var entities = asNoTracking ? Entities.Where(expression).AsNoTracking() : Entities.Where(expression);
            return await entities.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public virtual async Task AddAsync(T entity)
        {
            await Entities.AddAsync(entity).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        protected int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlRaw(sql, parameters);
        }

        public virtual async Task<int> RemoveAsync(T entity)
        {
            Remove(entity);
            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
