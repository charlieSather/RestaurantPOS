using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantPosApp
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        public RepositoryBase(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }
        public IQueryable<T> FindAll()
        {
            return ApplicationDbContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return ApplicationDbContext.Set<T>().Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            ApplicationDbContext.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            ApplicationDbContext.Set<T>().AddRange(entities);
        }
        public void Update(T entity)
        {
            ApplicationDbContext.Set<T>().Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
            ApplicationDbContext.Set<T>().UpdateRange(entities);
        }
        public void Delete(T entity)
        {
            ApplicationDbContext.Set<T>().Remove(entity);
        }
    }
}
