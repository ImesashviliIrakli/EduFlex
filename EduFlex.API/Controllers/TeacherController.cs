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
        var data = await _service.GetAllAsync();
        return CreateResponse(data);
    }

    [HttpGet("{teacherId:int}")]
    public async Task<IActionResult> Get(int teacherId)
    {
        var data = await _service.GetByIdAsync(teacherId);
        return CreateResponse(data);
    }

    [HttpGet("GetByUserId")]
    public async Task<IActionResult> GetByUserId()
    {
        var data = await _service.GetByUserIdAsync(GetCurrentUserId());
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

        await _service.AddAsync(addTeacherDto);
        return CreateResponse();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateTeacherDto updateTeacherDto)
    {
        updateTeacherDto.UserId = GetCurrentUserId();
        updateTeacherDto.Email = GetCurrentUserEmail();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.UpdateAsync(updateTeacherDto);
        return CreateResponse();
    }

    [HttpDelete("{teacherId:int}")]
    public async Task<IActionResult> Delete(int teacherId)
    {
        await _service.DeleteAsync(teacherId, GetCurrentUserId());
        return CreateResponse();
    }
    #endregion
}
