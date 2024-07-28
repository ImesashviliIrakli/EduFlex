using Application.Interfaces.Services;
using Application.Models.Dtos.FacultyDtos;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class FacultyController : BaseController
{
    #region Injection
    private IFacultyService _service;
    public FacultyController(IFacultyService service)
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
    public async Task<IActionResult> Post([FromBody] AddFacultyDto addFacultyDto)
    {
        ModelStateValidator.ValidateModelState(ModelState);

        await _service.AddAsync(addFacultyDto);
        return CreateResponse();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateFacultyDto updateFacultyDto)
    {
        ModelStateValidator.ValidateModelState(ModelState);

        await _service.UpdateAsync(updateFacultyDto);
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
