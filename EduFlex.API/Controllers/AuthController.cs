using Application.Interfaces.Services;
using Application.Models.Auth;
using EduFlex.API.Enums;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authenticationService;
	private ResponseModel _response;
	public AuthController(IAuthService authenticationService)
	{
		_authenticationService = authenticationService;
		_response = new ResponseModel(Status.Success, "Success");
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(AuthRequest request)
	{
		_response.Result = await _authenticationService.Login(request);
		return Ok(_response);
	}

	[HttpPost("addUserByAdmin")]
	[Authorize(Roles = "Admin")]

	public async Task<IActionResult> AddUserByAdmin(RegistrationRequest request)
	{
		_response.Result = await _authenticationService.Register(request);
		return Ok(_response);
	}

	[HttpPost("register")]
	public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
	{
		_response.Result = await _authenticationService.Register(request);
		return Ok(_response);
	}
}
