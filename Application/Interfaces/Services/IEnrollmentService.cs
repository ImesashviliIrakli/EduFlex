using Application.Models.Dtos.EnrollmentDtos;

namespace Application.Interfaces.Services;

public interface IEnrollmentService
{
    Task<IEnumerable<EnrollmentDto>> GetEnrollmentsAsync();
    Task<EnrollmentDto> GetEnrollmentAsync(int enrollmentId);
    Task EnrollAsync(AddEnrollmentDto addEnrollmentDto);
    Task UnEnrollAsync(int id, string userId);
}
