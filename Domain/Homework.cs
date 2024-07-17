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
    public int StudentId { get; set; }
    [Required]
    public required string FileUrl { get; set; }
    [Required]
    public int Grade { get; set; }
}
