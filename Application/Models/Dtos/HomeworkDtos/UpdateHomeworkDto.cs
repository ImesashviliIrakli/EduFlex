using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.HomeworkDtos;

public class UpdateHomeworkDto
{
    [Required]
    public int Id { get; set; }
    [JsonIgnore]
    public required string StudentUserId { get; set; }
    [Required]
    public int EnrollmentId { get; set; }
    [Required]
    public required IFormFile File { get; set; }
    [JsonIgnore]
    public required string FileUrl { get; set; }
}
