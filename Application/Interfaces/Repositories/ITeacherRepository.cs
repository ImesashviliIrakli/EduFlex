using Domain;

namespace Application.Interfaces.Repositories;

public interface ITeacherRepository : IRepository<Teacher>
{
    Task<Teacher> GetByUserIdAsync(string userId);
}
