using EduFlex.API.Enums;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduFlex.API.Controllers;

[Authorize]
public abstract class BaseController : ControllerBase
{
    protected ResponseModel _response;

    public BaseController()
    {
        _response = new ResponseModel(Status.Success, "Success");
    }

    // Get the current user ID from the claims
    protected string GetCurrentUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    // Get the current user's email from the claims
    protected string GetCurrentUserEmail()
    {
        return User.FindFirstValue(ClaimTypes.Email);
    }

    // Method to set the response
    protected IActionResult CreateResponse(object? data = null)
    {
        _response.Result = data;
        return Ok(_response);
    }
}
