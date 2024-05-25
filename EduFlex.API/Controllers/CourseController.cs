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
	public class CourseController : ControllerBase
	{
		private ICourseRepository _courseRepository;
		private ResponseModel _response;
		public CourseController(ICourseRepository courseRepository)
		{
			_courseRepository = courseRepository;
			_response = new ResponseModel(Status.Success, "Success");
		}

		[HttpGet("Get")]
		[AllowAnonymous]
		public async Task<IActionResult> Get()
		{
			_response.Result = await _courseRepository.GetAllAsync();
			return Ok(_response);
		}

		[HttpGet("{id:int}")]
		[AllowAnonymous]
		public async Task<IActionResult> Get(int id)
		{
			_response.Result = await _courseRepository.GetByIdAsync(id);
			return Ok(_response);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Course entity)
		{
			_response.Result = await _courseRepository.AddAsync(entity);
			return Ok(_response);
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			_response.Result = await _courseRepository.DeleteAsync(id);
			return Ok(_response);
		}

		[HttpPut]
		public async Task<IActionResult> Put(int id, Course entity)
		{
			_response.Result = await _courseRepository.UpdateAsync(id, entity);
			return Ok(_response);
		}
	}
}
