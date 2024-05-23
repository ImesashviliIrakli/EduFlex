namespace Application.Interfaces.Repositories;
public interface IRepository<T>
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<T> GetByIdAsync(int id);
	Task AddAsync(T obj);
	Task DeleteAsync(int id);
	Task Save();
}
