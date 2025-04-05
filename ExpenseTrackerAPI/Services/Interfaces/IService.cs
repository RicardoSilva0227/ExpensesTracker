using ExpenseTrackerAPI.Models;
using System.Linq.Expressions;

namespace ExpenseTrackerAPI.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAllAsync(int pageSize = 10, int pageNumber = 1, params Expression<Func<T, object>>[] includes);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();

    }
}
