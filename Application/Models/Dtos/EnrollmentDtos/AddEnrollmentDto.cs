using System.Text.Json.Serialization;

namespace Application.Models.Dtos.EnrollmentDtos;

public class AddEnrollmentDto
{
    public string StudentUserId { get; set; }
    [JsonIgnore]
    public int StudentId { get; set; }
    public int TeacherCourseMapId { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; }
}
