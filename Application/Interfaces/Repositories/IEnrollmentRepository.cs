using Application.Models.Dtos.EnrollmentDtos;
using Domain;

namespace Application.Interfaces.Repositories;

public interface IEnrollmentRepository : IRepository<Enrollment>
{
    public Task<IEnumerable<Enrollment>> GetByStudentId(int studentId);
}

