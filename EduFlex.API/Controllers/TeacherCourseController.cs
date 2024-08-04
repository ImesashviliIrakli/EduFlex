using Application.Interfaces.Services;
using Application.Models.Dtos;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Teacher")]
public class TeacherCourseController : BaseController
{
    #region Injection
    private readonly ITeacherCourseService _service;
    public TeacherCourseController(ITeacherCourseService service)
    {
        _service = service;
    }
    #endregion

    #region Read
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAllAsync();
        return CreateResponse(data);
    }

    [HttpGet("GetByUserId")]
    public async Task<IActionResult> GetByUserId()
    {
        var data = await _service.GetByUserIdAsync(GetCurrentUserId());
        return CreateResponse(data);
    }


    [HttpGet("{teacherCourseId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int teacherCourseId)
    {
        var data = await _service.GetByIdAsync(teacherCourseId);
        return CreateResponse(data);
    }
    #endregion

    #region Write
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddTeacherCourseDto addTeacherCourseDto)
    {
        addTeacherCourseDto.UserId = GetCurrentUserId();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.AddAsync(addTeacherCourseDto);
        return CreateResponse();
    }

    [HttpDelete("{teacherCourseId:int}")]
    public async Task<IActionResult> Delete(int teacherCourseId)
    {
        await _service.DeleteAsync(teacherCourseId, GetCurrentUserId());
        return CreateResponse();
    }
    #endregion
}
