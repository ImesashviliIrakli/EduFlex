using Application.Interfaces.Services;
using Application.Models.Dtos.FacultyDtos;
using EduFlex.API.Enums;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize(Roles = "Admin")]
	public class FacultyController : ControllerBase
	{
		private IFacultyService _service;
		private ResponseModel _response;
		public FacultyController(IFacultyService service)
		{
			_service = service;
			_response = new ResponseModel(Status.Success, "Success");
		}

		[HttpGet("Get")]
		[AllowAnonymous]
		public async Task<IActionResult> Get()
		{
			_response.Result = await _service.GetAllAsync();
			return Ok(_response);
		}

		[HttpGet("{id:int}")]
		[AllowAnonymous]
		public async Task<IActionResult> Get(int id)
		{
			_response.Result = await _service.GetByIdAsync(id);
			return Ok(_response);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] AddFacultyDto entity)
		{
			_response.Result = await _service.AddAsync(entity);
			return Ok(_response);
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			_response.Result = await _service.DeleteAsync(id);
			return Ok(_response);
		}

		[HttpPut]
		public async Task<IActionResult> Put(int id, [FromBody] UpdateFacultyDto entity)
		{
			_response.Result = await _service.UpdateAsync(id, entity);
			return Ok(_response);
		}
	}
}
