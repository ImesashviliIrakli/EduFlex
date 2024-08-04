using Application.Models.Dtos;

namespace Application.Interfaces.Services;

public interface ITeacherCourseService
{
    Task<IEnumerable<TeacherCourseDto>> GetAllAsync();
    Task<IEnumerable<TeacherCourseDto>> GetByUserIdAsync(string teacherUserId);
    Task<TeacherCourseDto> GetByIdAsync(int id);
    Task AddAsync(AddTeacherCourseDto addTeacherCourseDto);
    Task DeleteAsync(int id, string userId);
}
