using Application.Models.Dtos.AssignmentDtos;

namespace Application.Interfaces.Services;

public interface IAssignmentService
{
    Task<List<AssignmentDto>> GetAssignmentsAsync(int teacherCourseId, bool isActive);
    Task<AssignmentDto> GetAssignmentByIdAsync(int assignmentId);
    Task CreateAssignmentAsync(AddAssignmentDto addAssignmentDto);
    Task UpdateAssignmentAsync(UpdateAssignmentDto updateAssignmentDto);
    Task DeleteAssignmentAsync(int assignmentId);
}
