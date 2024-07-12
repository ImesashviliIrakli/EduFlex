using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos;

public class AddTeacherCourseDto
{
    [JsonIgnore]
    public string UserId { get; set; }
    [Required]
    public int CourseId { get; set; }
    [Required]
    public int TeacherId { get; set; }
}
