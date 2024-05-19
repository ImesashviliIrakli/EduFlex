using IdentityModel.Client;

namespace Application.Interfaces.Services;

public interface ITokenService
{
    Task<TokenResponse> GetToken(string scope);
}