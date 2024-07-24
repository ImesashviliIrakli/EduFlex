using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repository;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly AppDBContext _context;
    public AssignmentRepository(AppDBContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Assignment assignment)
    {
        await _context.Assignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Assignment assignment)
    {
        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task<Assignment> GetAssignmentByIdAsync(int assignmentId)
    {
        return await _context.Assignments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == assignmentId);
    }

    public async Task<List<Assignment>> GetAssignmentsAsync(int teacherCourseId, bool isActive)
    {
        return await _context.Assignments.Where(x => x.TeacherCourseId == teacherCourseId && x.IsActive == isActive).ToListAsync();
    }

    public async Task UpdateAsync(Assignment assignment)
    {
        _context.Assignments.Update(assignment);
        await _context.SaveChangesAsync();
    }
}
