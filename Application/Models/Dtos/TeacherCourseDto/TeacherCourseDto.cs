using Domain;

namespace Application.Models.Dtos;

public class TeacherCourseDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
	public Course Course { get; set; }
}
