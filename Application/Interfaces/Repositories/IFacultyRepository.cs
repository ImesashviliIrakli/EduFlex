using Domain;

namespace Application.Interfaces.Repositories;
public interface IFacultyRepository : IRepository<Faculty>
{
	void Update(Faculty obj);
}
