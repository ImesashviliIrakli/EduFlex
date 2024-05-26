using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.FacultyDtos;

public class FacultyDto
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
}
