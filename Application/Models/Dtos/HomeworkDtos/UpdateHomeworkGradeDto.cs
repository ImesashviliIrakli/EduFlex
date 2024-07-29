using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.HomeworkDtos;

public class UpdateHomeworkGradeDto
{
    [Required]
    public int Id { get; set; }
    [JsonIgnore]
    public required string TeacherUserId { get; set; }
    [Required]
    public int Grade { get; set; }
}
