using Domain;

namespace Application.Interfaces.Repositories;
public interface ICourseRepository : IRepository<Course>
{
	void Update(Course obj);
}
