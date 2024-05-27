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

    public async Task<IEnumerable<Enrollment>> GetByStudentId(int studentId)
    {
        return await _context.Enrollments.Where(x => x.StudentId == studentId).ToListAsync();
    }
}
