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
    [HttpGet("Get")]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAllAsync();
        return CreateResponse(data);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var data = await _service.GetByIdAsync(id);
        return CreateResponse(data);
    }
    #endregion

    #region Write
    [HttpPost]
    public async Task<IActionResult> Post([FromForm] AddCourseDto addCourseDto)
    {
        ModelStateValidator.ValidateModelState(ModelState);

        await _service.AddAsync(addCourseDto);
        return CreateResponse();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateCourseDto updateCourseDto)
    {
        ModelStateValidator.ValidateModelState(ModelState);

        await _service.UpdateAsync(updateCourseDto);
        return CreateResponse();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return CreateResponse();
    }
    #endregion
}
