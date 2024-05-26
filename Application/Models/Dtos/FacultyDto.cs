using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos;

public class FacultyDto
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
}
