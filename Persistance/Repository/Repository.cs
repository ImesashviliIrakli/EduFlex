using Application.Exceptions;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistance.Repository;
public class Repository<T> : IRepository<T> where T : class
{
	private readonly AppDBContext _db;
	private DbSet<T> dbSet;
	public Repository(AppDBContext db)
	{
		_db = db;
		this.dbSet = _db.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null)
	{
		IQueryable<T> query = dbSet;

		if (includeProperties != null)
		{
			foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProp);
			}
		}

		return await query.ToListAsync();
	}

	public async Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
	{
		IQueryable<T> query = dbSet;

		if (filter != null)
		{
			query = query.Where(filter);
		}

		if (includeProperties != null)
		{
			foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProp);
			}
		}

		return await query.FirstOrDefaultAsync();
	}
	public async Task<T> AddAsync(T entity)
	{
		await dbSet.AddAsync(entity);
		await _db.SaveChangesAsync();

		return entity;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var entity = await dbSet.FindAsync(id);
		if (entity != null)
		{
			dbSet.Remove(entity);
			await _db.SaveChangesAsync();
			return true;
		}

		return false;
	}

	public async Task<T> UpdateAsync(int id, T entity)
	{
		dbSet.Update(entity);
		await _db.SaveChangesAsync();

		return entity;

		throw new BadRequestException($"Could not find entity to update");
	}
}
