﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.EnrollmentDtos;

public class UpdateEnrollmentDto
{
    [Required]
    public int Id { get; set; }
    [JsonIgnore]
    public required string StudentUserId { get; set; }
    [Required]
    public int TeacherCourseMapId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int Status { get; set; }
}
