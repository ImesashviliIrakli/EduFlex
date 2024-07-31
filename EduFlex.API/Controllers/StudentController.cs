using Application.Interfaces.Services;
using Application.Models.Dtos.StudentDtos;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Student")]
public class StudentController : BaseController
{
    #region Injection
    private readonly IStudentService _service;
    public StudentController(IStudentService service)
    {
        _service = service;
    }
    #endregion

    #region Read
    [HttpGet]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetStudentsAsync();
        return CreateResponse(data);
    }

    [HttpGet("GetStudent")]
    public async Task<IActionResult> GetStudent()
    {
        var data = await _service.GetStudentByUserIdAsync(GetCurrentUserId());
        return CreateResponse(data);
    }
    #endregion

    #region Write
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddStudentDto addStudentDto)
    {
        addStudentDto.UserId = GetCurrentUserId();
        addStudentDto.Email = GetCurrentUserEmail();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.CreateStudentProfileAsync(addStudentDto);
        return CreateResponse();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateStudentDto updateStudentDto)
    {
        updateStudentDto.UserId = GetCurrentUserId();
        updateStudentDto.Email = GetCurrentUserEmail();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.UpdateStudentProfileAsync(updateStudentDto);
        return CreateResponse();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        await _service.DeleteStudentProfileAsync(GetCurrentUserId());
        return CreateResponse();
    }
    #endregion
}
