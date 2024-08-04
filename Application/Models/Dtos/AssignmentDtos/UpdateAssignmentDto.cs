using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.AssignmentDtos;

public class UpdateAssignmentDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Description { get; set; }
    public IFormFile? File { get; set; }
    [JsonIgnore]
    public string? FileUrl { get; set; }
    [Required]
    public int MaxGrade { get; set; }
    [Required]
    public int MinGrade { get; set; }
    [Required]
    public bool IsActive { get; set; }
}
