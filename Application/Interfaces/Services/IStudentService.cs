using Application.Models.Dtos.StudentDtos;

namespace Application.Interfaces.Services;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetStudentsAsync();
    Task<StudentDto> GetStudentByUserIdAsync(string userId);
    Task CreateStudentProfileAsync(AddStudentDto addStudentDto);
    Task UpdateStudentProfileAsync(UpdateStudentDto updateStudentDto);
    Task DeleteStudentProfileAsync(string userId);
}
