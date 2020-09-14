using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using School.Entities;

namespace School.Services.Interfaces
{
    public interface ISchoolService<T> where T : class
    {
        IQueryable<T> GetAll(bool asNoTracking = true, bool includeChildren = false);
        IEnumerable<T> GetAllEnumerable(bool asNoTracking = true, bool includeChildren = false);
        IQueryable<T> FindByCriteria(Expression<Func<T, bool>> expression, bool asNoTracking = true, bool includeChildren = false);
        T Update(T entity);
        T Remove(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> FindFirstByCriteriaAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true);
        Task AddAsync(T entity);
        Task<int> RemoveAsync(T entity);
    }
}