namespace Application.Models.Dtos.AssignmentDtos;

public class AssignmentDto
{
    public int Id { get; set; }
    public int TeacherCourseId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string FileUrl { get; set; }
    public int MaxGrade { get; set; }
    public int MinGrade { get; set; }
    public bool IsActive { get; set; }
}
