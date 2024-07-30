using Application.Interfaces.Services;
using Application.Models.Dtos.EnrollmentDtos;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EnrollmentController : BaseController
{
    #region Injection
    private readonly IEnrollmentService _service;
    public EnrollmentController(IEnrollmentService service)
    {
        _service = service;
    }
    #endregion

    #region Read
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetEnrollmentsAsync();
        return CreateResponse(data);
    }

    [HttpGet("{enrollmentId:int}")]
    public async Task<IActionResult> Get(int enrollmentId)
    {
        var data = await _service.GetEnrollmentAsync(enrollmentId);
        return CreateResponse(data);
    }
    #endregion

    #region Write
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddEnrollmentDto addEnrollmentDto)
    {
        addEnrollmentDto.StudentUserId = GetCurrentUserId();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.EnrollAsync(addEnrollmentDto);
        return CreateResponse();
    }

    [HttpDelete("{enrollmentId:int}")]
    public async Task<IActionResult> Delete(int enrollmentId)
    {
        await _service.UnEnrollAsync(enrollmentId, GetCurrentUserId());
        return CreateResponse();
    }
    #endregion
}
