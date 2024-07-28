using Application.Models.Dtos.TeacherDtos;

namespace Application.Interfaces.Services;

public interface ITeacherService
{
    Task<IEnumerable<TeacherDto>> GetAllAsync();
    Task<TeacherDto> GetByIdAsync(int id);
    Task<TeacherDto> GetByUserIdAsync(string userId);
    Task AddAsync(AddTeacherDto addTeacherDto);
    Task DeleteAsync(int id, string userId);
    Task UpdateAsync(UpdateTeacherDto updateTeacherDto);
}
