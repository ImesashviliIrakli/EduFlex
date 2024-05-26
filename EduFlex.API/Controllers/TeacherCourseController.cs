using Application.Interfaces.Services;
using Application.Models.Dtos;
using Application.Models.Dtos.StudentDtos;
using EduFlex.API.Enums;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherCourseController : ControllerBase
{
    private readonly ITeacherCourseService _service;
    private ResponseModel _response;
    public TeacherCourseController(ITeacherCourseService service)
    {
        _service = service;
        _response = new ResponseModel(Status.Success, "Success");
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _response.Result = await _service.GetAllAsync();
        return Ok(_response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        _response.Result = await _service.GetByIdAsync(id);
        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddTeacherCourseDto body)
    {
        body.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        _response.Result = await _service.AddAsync(body);
        return Ok(_response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        _response.Result = await _service.DeleteAsync(id, userId);
        return Ok(_response);
    }
}
