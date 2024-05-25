using Application.Interfaces.Repositories;
using Domain;
using EduFlex.API.Enums;
using EduFlex.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class FacultyController : ControllerBase
	{
		private IFacultyRepository _facultyRepository;
		private ResponseModel _response;
		public FacultyController(IFacultyRepository facultyRepository)
		{
			_facultyRepository = facultyRepository;
			_response = new ResponseModel(Status.Success, "Success");
		}

		[HttpGet("Get")]
		[AllowAnonymous]
		public async Task<IActionResult> Get()
		{
			_response.Result = await _facultyRepository.GetAllAsync();
			return Ok(_response);
		}

		[HttpGet("{id:int}")]
		[AllowAnonymous]
		public async Task<IActionResult> Get(int id)
		{
			_response.Result = await _facultyRepository.GetByIdAsync(id);
			return Ok(_response);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Faculty entity)
		{
			_response.Result = await _facultyRepository.AddAsync(entity);
			return Ok(_response);
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			_response.Result = await _facultyRepository.DeleteAsync(id);
			return Ok(_response);
		}

		[HttpPut]
		public async Task<IActionResult> Put(int id, Faculty entity)
		{
			_response.Result = await _facultyRepository.UpdateAsync(id, entity);
			return Ok(_response);
		}
	}
}
