using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Assignment
{
    [Key]
    public int Id { get; set; }
    [Required]

    public int TeacherCourseId { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public required string FileUrl { get; set; }
    [Required]
    public int MaxGrade { get; set; }
    [Required]
    public int MinGrade { get; set; }

}
