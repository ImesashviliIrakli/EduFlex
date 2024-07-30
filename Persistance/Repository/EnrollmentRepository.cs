using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repository;

public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
{
    private readonly AppDBContext _context;
    public EnrollmentRepository(AppDBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Enrollment>> GetByStudentUserIdAsync(string studentUserId)
    {
        return await _context.Enrollments.Where(x => x.StudentUserId.Equals(studentUserId)).ToListAsync();
    }

    public async Task<Enrollment> GetByStudentUserIdAndEnrollmentIdAsync(string studentUserId, int enrollmentId)
    {
        return await _context.Enrollments
            .FirstOrDefaultAsync(
            x => 
            x.StudentUserId.Equals(studentUserId) &&
            x.Id.Equals(enrollmentId)
            );
    }
}
