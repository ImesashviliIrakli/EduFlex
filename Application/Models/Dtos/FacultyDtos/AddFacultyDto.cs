using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.FacultyDtos;

public class AddFacultyDto
{
    [Required]
    public required string Name { get; set; }
}
