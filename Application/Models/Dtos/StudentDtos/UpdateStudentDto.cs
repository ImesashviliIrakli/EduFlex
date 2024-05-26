using System.Text.Json.Serialization;

namespace Application.Models.Dtos.StudentDtos;

public class UpdateStudentDto
{
    public int Id { get; set; }
    [JsonIgnore]
    public string UserId { get; set; }
    [JsonIgnore]
    public string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string PrivateNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}
