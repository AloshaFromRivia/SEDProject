using Microsoft.EntityFrameworkCore;
using SEDProject.Models.Entities;
using System.Linq.Expressions;

namespace SEDProject.Models.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<EntityState> CreateAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<EntityState> RemoveAsync(Guid id);
        Task<EntityState> UpdateAsync(T entity);
    }
}
