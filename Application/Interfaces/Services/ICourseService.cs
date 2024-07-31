using Application.Models.Dtos.CourseDtos;

namespace Application.Interfaces.Services;

public interface ICourseService
{
	Task<IEnumerable<CourseDto>> GetCoursesAsync();
	Task<CourseDto> GetCourseByIdAsync(int id);
	Task CreateCourseAsync(AddCourseDto addCourseDto);
	Task UpdateCourseAsync(UpdateCourseDto updateCourseDto);
    Task DeleteCourseAsync(int id);
}
