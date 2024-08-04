using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.HomeworkDtos;

public class UploadHomeworkDto
{
    [Required]
    public int EnrollmentId { get; set; }
    [Required]
    public int AssignmentId { get; set; }
    [Required]
    public int TeacherCourseId { get; set; }
    [JsonIgnore]
    public required string StudentUserId { get; set; }
    [Required]
    public required IFormFile File { get; set; }
    [JsonIgnore] 
    public string? FileUrl { get; set; } = string.Empty;
}
