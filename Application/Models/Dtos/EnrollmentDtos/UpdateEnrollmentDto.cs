using System.Text.Json.Serialization;

namespace Application.Models.Dtos.EnrollmentDtos;

public class UpdateEnrollmentDto
{
    public int Id { get; set; }
    public string StudentUserId { get; set; }
    [JsonIgnore]
    public int StudentId { get; set; }
    public int TeacherCourseMapId { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; }
}
