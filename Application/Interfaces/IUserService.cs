
using Application.Models;

namespace Application.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetUsers();
}
