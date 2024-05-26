using Application.Models.Dtos;

namespace Application.Interfaces.Services;

public interface ITeacherCourseService
{
    Task<IEnumerable<TeacherCourseDto>> GetAllAsync();
    Task<TeacherCourseDto> GetByIdAsync(int id);
    Task<TeacherCourseDto> AddAsync(AddTeacherCourseDto entity);
    Task<bool> DeleteAsync(int id, string userId);
}
