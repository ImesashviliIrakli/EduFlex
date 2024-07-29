using System.Text.Json.Serialization;

namespace Application.Models.Dtos.HomeworkDtos;

public class HomeworkDto
{
    public int Id { get; set; }
    public int EnrollmentId { get; set; }
    public int AssignmentId { get; set; }
    [JsonIgnore]
    public int StudentUserId { get; set; }
    public required string FileUrl { get; set; }
    public int Grade { get; set; } = 0;
}
