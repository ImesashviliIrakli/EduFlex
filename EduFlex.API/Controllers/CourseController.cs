using Application.Interfaces.Services;
using Application.Models.Dtos.CourseDtos;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CourseController : BaseController
{
    #region Injection
    private readonly ICourseService _service;
    public CourseController(ICourseService service)
    {
        _service = service;
    }
    #endregion

    #region Read
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetCoursesAsync();
        return CreateResponse(data);
    }

    [HttpGet("{courseId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int courseId)
    {
        var data = await _service.GetCourseByIdAsync(courseId);
        return CreateResponse(data);
    }
    #endregion

    #region Write
    [HttpPost]
    public async Task<IActionResult> Post([FromForm] AddCourseDto addCourseDto)
    {
        ModelStateValidator.ValidateModelState(ModelState);

        await _service.CreateCourseAsync(addCourseDto);
        return CreateResponse();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromForm] UpdateCourseDto updateCourseDto)
    {
        ModelStateValidator.ValidateModelState(ModelState);

        await _service.UpdateCourseAsync(updateCourseDto);
        return CreateResponse();
    }

    [HttpDelete("{courseId:int}")]
    public async Task<IActionResult> Delete(int courseId)
    {
        await _service.DeleteCourseAsync(courseId);
        return CreateResponse();
    }
    #endregion
}
