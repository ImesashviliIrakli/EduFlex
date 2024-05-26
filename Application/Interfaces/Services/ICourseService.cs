using Application.Models.Dtos.CourseDtos;

namespace Application.Interfaces.Services
{
	public interface ICourseService
	{
		Task<IEnumerable<CourseDto>> GetAllAsync();
		Task<CourseDto> GetByIdAsync(int id);
		Task<CourseDto> AddAsync(AddCourseDto entity);
		Task<bool> DeleteAsync(int id);
		Task<CourseDto> UpdateAsync(int id, UpdateCourseDto entity);
	}
}
