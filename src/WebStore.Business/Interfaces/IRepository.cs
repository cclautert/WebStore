using System.Linq.Expressions;
using WebStore.Business.Models;

namespace WebStore.Business.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(Guid id);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
