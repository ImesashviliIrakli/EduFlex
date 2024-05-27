namespace Application.Models.Dtos.EnrollmentDtos;

public class EnrollmentDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int TeacherCourseMapId { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; }
}
