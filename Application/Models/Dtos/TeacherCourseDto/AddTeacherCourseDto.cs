using System.Text.Json.Serialization;

namespace Application.Models.Dtos;

public class AddTeacherCourseDto
{
    [JsonIgnore]
    public string UserId { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
}
