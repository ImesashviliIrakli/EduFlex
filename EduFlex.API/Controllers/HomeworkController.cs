using Application.Interfaces.Services;
using Application.Models.Dtos.HomeworkDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Student")]
public class HomeworkController : BaseController
{
    #region Injection
    private readonly IHomeworkService _service;
    public HomeworkController(IHomeworkService service)
    {
        _service = service;
    }
    #endregion

    #region Read
    [HttpGet("{teacherCourseId:int}")]
    public async Task<IActionResult> GetHomeworks(int teacherCourseId)
    {
        var data = await _service.GetHomeworksAsync(GetCurrentUserId(), teacherCourseId);
        
        return CreateResponse(data);
    }
    #endregion

    #region Write

    [HttpPost("UploadHomework")]
    public async Task<IActionResult> UploadHomework([FromBody] UploadHomeworkDto uploadHomeworkDto)
    {
        uploadHomeworkDto.StudentUserId = GetCurrentUserId();
        await _service.UploadHomeworkAsync(uploadHomeworkDto);

        return CreateResponse();
    }

    [HttpPut("UpdateHomework")]
    public async Task<IActionResult> UpdateHomework([FromBody] UpdateHomeworkDto updateHomeworkDto)
    {
        updateHomeworkDto.StudentUserId = GetCurrentUserId();
        await _service.UpdateHomeworkAsync(updateHomeworkDto);

        return CreateResponse();
    }

    [HttpPut("UpdateHomeworkGrade")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> UpdateHomeworkGrade([FromBody] UpdateHomeworkGradeDto updateHomeworkGradeDto)
    {
        updateHomeworkGradeDto.TeacherUserId = GetCurrentUserId();
        await _service.UpdateHomeworkGradeAsync(updateHomeworkGradeDto);

        return CreateResponse();
    }

    [HttpDelete("DeleteHomework/{homeworkId:int}")]
    public async Task<IActionResult> DeleteHomework(int homeworkId)
    {
        await _service.DeleteHomeworkAsync(homeworkId, GetCurrentUserId());

        return CreateResponse();
    }
    #endregion
}
