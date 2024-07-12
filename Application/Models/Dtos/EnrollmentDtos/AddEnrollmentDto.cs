﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.EnrollmentDtos;

public class AddEnrollmentDto
{
    [JsonIgnore]
    public string StudentUserId { get; set; }
    [JsonIgnore]
    public int StudentId { get; set; }
    [Required]
    public int TeacherCourseMapId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int Status { get; set; }
}
