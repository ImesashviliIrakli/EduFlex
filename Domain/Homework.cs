using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Homework
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int EnrollmentId { get; set; }
    [Required]
    public int AssignmentId { get; set; }
    [Required]
    public int TeacherCourseId { get; set; }
    [Required]
    public required string StudentUserId { get; set; }
    [Required]
    public required string FileUrl { get; set; }
    public int Grade { get; set; }
}