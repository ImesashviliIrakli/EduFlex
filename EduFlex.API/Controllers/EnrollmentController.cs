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
        var data = await _service.GetAllAsync();
        return CreateResponse(data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var data = await _service.GetByIdAsync(id);
        return CreateResponse(data);
    }
    #endregion

    #region Write
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddEnrollmentDto addEnrollmentDto)
    {
        addEnrollmentDto.StudentUserId = GetCurrentUserId();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.AddAsync(addEnrollmentDto);
        return CreateResponse();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateEnrollmentDto updateEnrollmentDto)
    {
        updateEnrollmentDto.StudentUserId = GetCurrentUserId();

        ModelStateValidator.ValidateModelState(ModelState);

        await _service.UpdateAsync(updateEnrollmentDto);
        return CreateResponse();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id, GetCurrentUserId());
        return CreateResponse();
    }
    #endregion
}
