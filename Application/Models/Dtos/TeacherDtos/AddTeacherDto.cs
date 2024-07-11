using System.Text.Json.Serialization;

namespace Application.Models.Dtos.TeacherDtos;

public class AddTeacherDto
{
    [JsonIgnore]
    public string UserId { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string PrivateNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int YearsOfExperience { get; set; }
}
