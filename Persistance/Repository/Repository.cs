using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repository;
public class Repository<T> : IRepository<T> where T : class
{
	private readonly AppDBContext _dbContext;
	private DbSet<T> dbSet;
	public Repository(AppDBContext db)
	{
		_dbContext = db;
		this.dbSet = _dbContext.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await dbSet.ToListAsync();
	}

	public async Task<T> GetByIdAsync(int id)
	{
		return await dbSet.FindAsync(id);
	}
	public async Task AddAsync(T obj)
	{
		await dbSet.AddAsync(obj);
	}

	public async Task DeleteAsync(int id)
	{
		var entity = await dbSet.FindAsync(id);

		if (entity != null)
		{
			dbSet.Remove(entity);
		}
	}

	public async Task Save()
	{
		await _dbContext.SaveChangesAsync();
	}
}
