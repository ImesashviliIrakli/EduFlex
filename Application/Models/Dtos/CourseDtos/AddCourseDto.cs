namespace Application.Models.Dtos.CourseDtos;

public class AddCourseDto
{
	public string Title { get; set; }
	public string Description { get; set; }
	public int Price { get; set; }
	public string ImageUrl { get; set; }
    public int FacultyId { get; set; }
}
