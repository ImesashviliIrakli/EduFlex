using Application.Models.Dtos.EnrollmentDtos;

namespace Application.Interfaces.Services;

public interface IEnrollmentService
{
    Task<IEnumerable<EnrollmentDto>> GetAllAsync();
    Task<EnrollmentDto> GetByIdAsync(int id);
    Task AddAsync(AddEnrollmentDto addEnrollmentDto);
    Task DeleteAsync(int id, string userId);
    Task UpdateAsync(UpdateEnrollmentDto updateEnrollmentDto);
}
