using Application.Interfaces.Services;
using Application.Models.Dtos.AssignmentDtos;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Teacher")]
public class AssignmentController : BaseController
{
    #region Injection
    private readonly IAssignmentService _service;
    private const bool IsActive = true;
    public AssignmentController(IAssignmentService service)
    {
        _service = service;
    }
    #endregion

    #region Read
    [Authorize(Roles = "Teacher,Student,Admin")]
    [HttpGet("GetByTeacherCourseId/{teacherCourseId:int}")]
    public async Task<IActionResult> GetByTeacherCourseId(int teacherCourseId)
    {
        var data = await _service.GetAssignmentsAsync(teacherCourseId, IsActive);
        return CreateResponse(data);
    }

    [HttpGet("GetByAssignmentId/{assignmentId:int}")]
    public async Task<IActionResult> GetByAssignmentId(int assignmentId)
    {
        var data = await _service.GetAssignmentByIdAsync(assignmentId);
        return CreateResponse(data);
    }
    #endregion

    #region Write
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddAssignmentDto addAssignmentDto)
    {
        ModelStateValidator.ValidateModelState(ModelState);

        await _service.AddAsync(addAssignmentDto);
        return CreateResponse();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateAssignmentDto updateAssignmentDto)
    {
        ModelStateValidator.ValidateModelState(ModelState);

        await _service.UpdateAsync(updateAssignmentDto);
        return CreateResponse();
    }

    [HttpDelete("DleteByAssignmentId/{assignmentId:int}")]
    public async Task<IActionResult> DleteByAssignmentId(int assignmentId)
    {
        ModelStateValidator.ValidateModelState(ModelState);

        await _service.DeleteAsync(assignmentId);
        return CreateResponse();
    }
    #endregion
}
