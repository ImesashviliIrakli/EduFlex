using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.FacultyDtos;

public class UpdateFacultyDto
{
	[JsonIgnore]
	public int Id { get; set; }
	[Required]
	public required string Name { get; set; }
}
