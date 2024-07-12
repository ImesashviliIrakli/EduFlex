using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.StudentDtos;

public class UpdateStudentDto
{
    public int Id { get; set; }
    [JsonIgnore]
    public string UserId { get; set; }
    [JsonIgnore]
    public string Email { get; set; }
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public required string PhoneNumber { get; set; }
    [Required]
    public required string PrivateNumber { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
}
