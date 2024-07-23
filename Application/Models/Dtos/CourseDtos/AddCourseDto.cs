using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.CourseDtos;

public class AddCourseDto
{
	[Required]
	public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public IFormFile File { get; set; }
    [JsonIgnore]
    public string ImageUrl { get; set; }
    [Required]
    public int FacultyId { get; set; }
}
