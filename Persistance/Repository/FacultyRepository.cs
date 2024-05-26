using Application.Interfaces.Repositories;
using Domain;
using Persistance.Data;

namespace Persistance.Repository;
public class FacultyRepository : Repository<Faculty>, IFacultyRepository
{
	public FacultyRepository(AppDBContext db) : base(db)
	{ }
}
