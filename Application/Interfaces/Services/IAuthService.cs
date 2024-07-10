using Application.Models.Auth;
using Domain;

namespace Application.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
    Task<List<UserDto>> GetUsers(string roleName);
}