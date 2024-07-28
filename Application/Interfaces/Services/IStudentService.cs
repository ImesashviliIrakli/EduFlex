using Application.Models.Dtos.StudentDtos;

namespace Application.Interfaces.Services;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllAsync();
    Task<StudentDto> GetByIdAsync(int id);
    Task<StudentDto> GetByUserIdAsync(string userId);
    Task AddAsync(AddStudentDto addStudentDto);
    Task DeleteAsync(int id, string userId);
    Task UpdateAsync(UpdateStudentDto updateStudentDto);
}
