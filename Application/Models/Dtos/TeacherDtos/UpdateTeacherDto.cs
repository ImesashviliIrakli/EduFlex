using System.Text.Json.Serialization;

namespace Application.Models.Dtos.TeacherDtos;

public class UpdateTeacherDto
{
    public int Id { get; set; }
    [JsonIgnore]
    public string UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string PrivateNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int YearsOfExperience { get; set; }
}
