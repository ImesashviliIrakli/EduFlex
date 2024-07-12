using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.CourseDtos;

public class UpdateCourseDto
{
	[Required]
	public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    [Required]
    public int FacultyId { get; set; }
}
