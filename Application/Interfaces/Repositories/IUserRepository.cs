using Domain.Auth;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAll();
}
