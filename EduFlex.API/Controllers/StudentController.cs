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
        var data = await _service.GetAllAsync();
        return CreateResponse(data);
    }

    [HttpGet("{studentId:int}")]
    [Authorize(Roles = "Student,Teacher,Admin")]
    public async Task<IActionResult> Get(int studentId)
    {
        var data = await _service.GetByIdAsync(studentId);
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
    public async Task<IActionResult> Post([FromBody] AddStudentDto addStudentDto)
    {
        addStudentDto.UserId = GetCurrentUserId();
        addStudentDto.Email = GetCurrentUserEmail();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.AddAsync(addStudentDto);
        return CreateResponse();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateStudentDto updateStudentDto)
    {
        updateStudentDto.UserId = GetCurrentUserId();
        updateStudentDto.Email = GetCurrentUserEmail();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.UpdateAsync(updateStudentDto);
        return CreateResponse();
    }

    [HttpDelete("{studentId:int}")]
    public async Task<IActionResult> Delete(int studentId)
    {
        var userId = GetCurrentUserId();

        await _service.DeleteAsync(studentId, userId);
        return CreateResponse();
    }
    #endregion
}
