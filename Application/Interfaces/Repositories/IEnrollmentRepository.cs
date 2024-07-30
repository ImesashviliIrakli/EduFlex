using Application.Models.Dtos.EnrollmentDtos;
using Domain;

namespace Application.Interfaces.Repositories;

public interface IEnrollmentRepository : IRepository<Enrollment>
{
    Task<IEnumerable<Enrollment>> GetByStudentUserIdAsync(string studentUserId);
    Task<Enrollment> GetByStudentUserIdAndEnrollmentIdAsync(string studentUserId, int enrollmentId);
}

