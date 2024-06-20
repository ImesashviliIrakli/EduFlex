using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Dtos.CourseDtos;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository _courseRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<CourseService> _logger;

		public CourseService(ICourseRepository courseRepository, IMapper mapper, ILogger<CourseService> logger)
		{
			_courseRepository = courseRepository;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<CourseDto> AddAsync(AddCourseDto entity)
		{
			var course = _mapper.Map<Course>(entity);

			var add = await _courseRepository.AddAsync(course);

			_logger.LogInformation($"Course was added: {add.Id}");

			var result = _mapper.Map<CourseDto>(add);

			return result;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var delete = await _courseRepository.DeleteAsync(id);

			if (delete)
			{
				_logger.LogInformation($"Course was deleted: {id}");
			}
			else
			{
				_logger.LogError($"Course wasn't deleted: {id}");
			}

			return delete;
		}

		public async Task<IEnumerable<CourseDto>> GetAllAsync()
		{
			var courses = await _courseRepository.GetAllAsync(includeProperties: "Faculty");
			var result = _mapper.Map<IEnumerable<CourseDto>>(courses);

			return result;
		}

		public async Task<CourseDto> GetByIdAsync(int id)
		{
			var course = await _courseRepository.GetByIdAsync(filter: (u) => u.Id == id, includeProperties: "Faculty");
			var result = _mapper.Map<CourseDto>(course);

			return result;
		}

		public async Task<CourseDto> UpdateAsync(int id, UpdateCourseDto entity)
		{
			var course = _mapper.Map<Course>(entity);

			var update = await _courseRepository.UpdateAsync(id, course);

			_logger.LogInformation($"Course was updated: {id}");

			var result = _mapper.Map<CourseDto>(update);

			return result;
		}
	}
}
