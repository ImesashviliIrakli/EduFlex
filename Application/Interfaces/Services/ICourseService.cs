using Application.Models.Dtos.CourseDtos;

namespace Application.Interfaces.Services;

public interface ICourseService
{
	Task<IEnumerable<CourseDto>> GetAllAsync();
	Task<CourseDto> GetByIdAsync(int id);
	Task AddAsync(AddCourseDto addCourseDto);
	Task DeleteAsync(int id);
	Task UpdateAsync(UpdateCourseDto updateCourseDto);
}
