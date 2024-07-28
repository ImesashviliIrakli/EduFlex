using Application.Interfaces.Services;
using Application.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    #region Injection
    private readonly IAuthService _authenticationService;
    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    #endregion

    #region Read
    [HttpGet("getusers/{roleName}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers(string roleName)
    {
        var data = await _authenticationService.GetUsers(roleName);
        return CreateResponse(data);
    }
    #endregion

    #region Write
    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthRequest request)
    {
        var data = await _authenticationService.Login(request);
        return CreateResponse(data);
    }

    [HttpPost("addUserByAdmin")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> AddUserByAdmin(RegistrationRequest request)
    {
        var data = await _authenticationService.Register(request);
        return CreateResponse(data);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        var data = await _authenticationService.Register(request);
        return CreateResponse(data);
    }
    #endregion
}
