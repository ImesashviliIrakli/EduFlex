using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Models.Auth;
using Application.Options;
using Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services;

public class AuthService : IAuthService
{
    private readonly AuthDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtOptions _jwtOptions;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        AuthDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtOptions> jwtOptions,
            ILogger<AuthService> logger
        )
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _jwtOptions = jwtOptions.Value;
        _logger = logger;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new NotFoundException($"User with {request.Email} not found.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded == false)
            throw new BadRequestException($"Credentials for '{request.Email} aren't valid'.");

        JwtSecurityToken jwtSecurityToken = await GenerateTokenAsync(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        _logger.LogInformation($"{user.UserName} logged in successfully");
        return response;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var user = new IdentityUser
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    EmailConfirmed = true
                };

                var createUserResult = await _userManager.CreateAsync(user, request.Password);

                if (!createUserResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(BuildErrorMessage(createUserResult.Errors));
                }

                var addRoleResult = await _userManager.AddToRoleAsync(user, request.RoleName);

                if (!addRoleResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(BuildErrorMessage(addRoleResult.Errors));
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, request.RoleName)
                };

                var addClaimsResult = await _userManager.AddClaimsAsync(user, claims);

                if (!addClaimsResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(BuildErrorMessage(addClaimsResult.Errors));
                }

                await transaction.CommitAsync();
                return new RegistrationResponse { UserId = user.Id };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Todo: Consider logging the exception here
                throw new BadRequestException("Registration failed: " + ex.Message);
            }
        }
    }

    private string BuildErrorMessage(IEnumerable<IdentityError> errors)
    {
        var stringBuilder = new StringBuilder();
        foreach (var error in errors)
        {
            stringBuilder.AppendLine($"• {error.Description}");
        }
        return stringBuilder.ToString();
    }

    private async Task<JwtSecurityToken> GenerateTokenAsync(IdentityUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
           issuer: _jwtOptions.Issuer,
           audience: _jwtOptions.Audience,
           claims: claims,
           expires: DateTime.Now.AddMinutes(_jwtOptions.DurationInMinutes),
           signingCredentials: signingCredentials);

        return jwtSecurityToken;

    }
}

