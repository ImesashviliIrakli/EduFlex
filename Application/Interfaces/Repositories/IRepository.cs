namespace Application.Interfaces.Repositories;
public interface IRepository<T>
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<T> GetByIdAsync(int id);
	Task<T> AddAsync(T entity);
	Task<bool> DeleteAsync(int id);
	Task<T> UpdateAsync(int id, T entity);
}
