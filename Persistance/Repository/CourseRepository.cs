using Application.Interfaces.Repositories;
using Domain;
using Persistance.Data;

namespace Persistance.Repository;
public class CourseRepository : Repository<Course>, ICourseRepository
{
	private readonly AppDBContext _dbContext;
	public CourseRepository(AppDBContext db) : base(db)
	{
		_dbContext = db;
	}
}
