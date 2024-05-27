using Application.Models.Dtos.EnrollmentDtos;

namespace Application.Interfaces.Services;

public interface IEnrollmentService
{
    Task<IEnumerable<EnrollmentDto>> GetAllAsync();
    Task<EnrollmentDto> GetByIdAsync(int id);
    Task<EnrollmentDto> AddAsync(AddEnrollmentDto entity);
    Task<bool> DeleteAsync(int id, string userId);
    Task<EnrollmentDto> UpdateAsync(int id, UpdateEnrollmentDto entity);
}
