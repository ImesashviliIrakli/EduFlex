using Application.Interfaces.Services;
using Application.Models.Auth;
using Application.Options;
using Identity.Data;
using Microsoft.AspNetCore.Identity;
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

    public AuthService(
        AuthDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtOptions> jwtOptions
        )
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new Exception($"User with {request.Email} not found.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded == false)
        {
            throw new Exception($"Credentials for '{request.Email} aren't valid'.");
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        return response;
    }


    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            var user = new IdentityUser
            {
                Email = request.Email,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, request.RoleName);
                if (roleResult.Succeeded)
                {
                    await transaction.CommitAsync();
                    return new RegistrationResponse() { UserId = user.Id };
                }
                else
                {
                    await transaction.RollbackAsync();
                    StringBuilder str = new StringBuilder();
                    foreach (var err in roleResult.Errors)
                    {
                        str.AppendFormat("•{0}\n", err.Description);
                    }
                    throw new Exception($"Role assignment failed: {str}");
                }
            }
            else
            {
                await transaction.RollbackAsync();
                StringBuilder str = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    str.AppendFormat("•{0}\n", err.Description);
                }
                throw new Exception($"{str}");
            }
        }
    }

    public async Task<JwtSecurityToken> GenerateToken(IdentityUser applicationUser)
    {
        var roles = await _userManager.GetRolesAsync(applicationUser);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName),
            };

        claimList.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
           issuer: _jwtOptions.Issuer,
           audience: _jwtOptions.Audience,
           claims: claimList,
           expires: DateTime.Now.AddMinutes(_jwtOptions.DurationInMinutes),
           signingCredentials: signingCredentials);

        return jwtSecurityToken;

    }
}

