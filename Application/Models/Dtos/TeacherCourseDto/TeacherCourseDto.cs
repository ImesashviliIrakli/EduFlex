using Application.Models.Dtos.CourseDtos;
using Application.Models.Dtos.TeacherDtos;

namespace Application.Models.Dtos;

public class TeacherCourseDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public TeacherDto Teacher { get;set; } 
	public CourseDto Course { get; set; }
}
