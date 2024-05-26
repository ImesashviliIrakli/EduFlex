using Domain;

namespace Application.Interfaces.Repositories;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student> GetByUserIdAsync(string userId);
}
