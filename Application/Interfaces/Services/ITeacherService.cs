using Application.Models.Dtos;

namespace Application.Interfaces.Services;

public interface ITeacherService
{
    Task<IEnumerable<TeacherDto>> GetAllAsync();
    Task<TeacherDto> GetByIdAsync(int id);
    Task<TeacherDto> AddAsync(TeacherDto entity);
    Task<bool> DeleteAsync(int id, string userId);
    Task<TeacherDto> UpdateAsync(int id, TeacherDto entity);
}
