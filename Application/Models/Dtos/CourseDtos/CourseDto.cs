using Application.Models.Dtos.FacultyDtos;

namespace Application.Models.Dtos.CourseDtos;

public class CourseDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public int Price { get; set; }
	public string ImageUrl { get; set; }
	public FacultyDto Faculty { get; set; }
}
