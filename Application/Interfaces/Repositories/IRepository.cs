using System.Linq.Expressions;

namespace Application.Interfaces.Repositories;
public interface IRepository<T>
{
	Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
	Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
	Task<T> AddAsync(T entity);
	Task<bool> DeleteAsync(int id);
	Task<T> UpdateAsync(int id, T entity);
}
