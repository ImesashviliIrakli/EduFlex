using Domain;

namespace Application.Interfaces.Repositories;
public interface IFacultyRepository : IRepository<Faculty>
{
    Task<Faculty> GetById(int id);
}
