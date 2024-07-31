using Application.Interfaces.Services;
using Application.Models.Dtos.TeacherDtos;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Teacher")]
public class TeacherController : BaseController
{
    #region Injection
    private readonly ITeacherService _service;
    public TeacherController(ITeacherService service)
    {
        _service = service;
    }
    #endregion

    #region Read
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetTeachersAsync();
        return CreateResponse(data);
    }

    [HttpGet("GetTeacher")]
    public async Task<IActionResult> GetTeacher()
    {
        var data = await _service.GetTeacherByUserIdAsync(GetCurrentUserId());
        return CreateResponse(data);
    }
    #endregion

    #region Write
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddTeacherDto addTeacherDto)
    {
        addTeacherDto.UserId = GetCurrentUserId();
        addTeacherDto.Email = GetCurrentUserEmail();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.CreateTeacherProfileAsync(addTeacherDto);
        return CreateResponse();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateTeacherDto updateTeacherDto)
    {
        updateTeacherDto.UserId = GetCurrentUserId();
        updateTeacherDto.Email = GetCurrentUserEmail();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.UpdateTeacherProfileAsync(updateTeacherDto);
        return CreateResponse();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        await _service.DeleteTeacherProfileAsync(GetCurrentUserId());
        return CreateResponse();
    }
    #endregion
}
