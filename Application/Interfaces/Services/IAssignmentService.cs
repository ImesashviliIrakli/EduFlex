using Application.Models.Dtos.AssignmentDtos;

namespace Application.Interfaces.Services;

public interface IAssignmentService
{
    Task<List<AssignmentDto>> GetAssignmentsAsync(int teacherCourseId, bool isActive);
    Task<AssignmentDto> GetAssignmentByIdAsync(int assignmentId);
    Task AddAsync(AddAssignmentDto addAssignmentDto);
    Task UpdateAsync(UpdateAssignmentDto updateAssignmentDto);
    Task DeleteAsync(int assignmentId);
}
