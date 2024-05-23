using Application.Interfaces.Repositories;
using Domain;
using Persistance.Data;

namespace Persistance.Repository;
public class FacultyRepository : Repository<Faculty>, IFacultyRepository
{
	private readonly AppDBContext _dbContext;
	public FacultyRepository(AppDBContext db) : base(db)
	{
		_dbContext = db;
	}

	public void Update(Faculty obj)
	{
		_dbContext.Faculties.Update(obj);
	}
}
