using Application.Models.Dtos.StudentDtos;
using Application.Models.Dtos.TeacherDtos;

namespace Application.Interfaces.Services;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllAsync();
    Task<StudentDto> GetByIdAsync(int id);
    Task<StudentDto> AddAsync(AddStudentDto entity);
    Task<bool> DeleteAsync(int id, string userId);
    Task<StudentDto> UpdateAsync(int id, UpdateStudentDto entity);
}
