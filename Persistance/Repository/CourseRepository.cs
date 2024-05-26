using Application.Interfaces.Repositories;
using Domain;
using Persistance.Data;

namespace Persistance.Repository;
public class CourseRepository : Repository<Course>, ICourseRepository
{
	public CourseRepository(AppDBContext db) : base(db)
	{ }
}
