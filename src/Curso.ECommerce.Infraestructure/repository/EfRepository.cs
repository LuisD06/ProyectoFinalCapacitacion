using System.Linq.Expressions;
using Curso.ComercioElectronico.Domain.repository;

namespace Curso.ComercioElectronico.Infraestructure
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public EfRepository()
        {

        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll(bool asNoTracking = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}