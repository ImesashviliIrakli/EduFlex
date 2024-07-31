using Application.Models.Dtos.TeacherDtos;

namespace Application.Interfaces.Services;

public interface ITeacherService
{
    Task<IEnumerable<TeacherDto>> GetTeachersAsync();
    Task<TeacherDto> GetTeacherByUserIdAsync(string teacherUserId);
    Task CreateTeacherProfileAsync(AddTeacherDto addTeacherDto);
    Task UpdateTeacherProfileAsync(UpdateTeacherDto updateTeacherDto);
    Task DeleteTeacherProfileAsync(string teacherUserId);
}
