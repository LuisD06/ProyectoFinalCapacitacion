using System.Linq.Expressions;
using Curso.ECommerce.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Curso.ECommerce.Infraestructure
{
    public abstract class EfRepository<TEntity, TEntityId> : IRepository<TEntity, TEntityId> where TEntity : class
    {
        protected readonly ECommerceDbContext context;
        public IUnitOfWork UnitOfWork => context;

        public EfRepository(ECommerceDbContext context)
        {
            this.context = context;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public virtual IQueryable<TEntity> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return context.Set<TEntity>().AsNoTracking();
            }
            else
            {
                return context.Set<TEntity>().AsQueryable();
            }
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = GetAll();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<TEntity, object>(includeProperty);
            }

            return queryable;
        }

        public virtual async Task<TEntity> GetByIdAsync(TEntityId id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            context.Update(entity);

            return;
        }

    }
}