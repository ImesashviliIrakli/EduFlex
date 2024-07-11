namespace Application.Models.Dtos;

public class GetTeacherCourseDto
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public int TeacherName { get; set; }
    public int CourseId { get; set; }
    public int CourseName { get; set; }
}
