using Domain;

namespace Application.Interfaces.Repositories;

public interface IAssignmentRepository
{
    Task<List<Assignment>> GetAssignmentsAsync(int teacherCourseId, bool isActive);
    Task<Assignment> GetAssignmentByIdAsync(int assignmentId);
    Task AddAsync(Assignment assignment);
    Task UpdateAsync(Assignment assignment);
    Task DeleteAsync(Assignment assignment);
}
