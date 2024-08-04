using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public string FileUrl { get; set; } = string.Empty;
    [Required]
    public int MaxGrade { get; set; }
    [Required]
    public int MinGrade { get; set; }
    [Required]
    public bool IsActive { get; set; }

    [ForeignKey("TeacherCourseId")]
    public TeacherCourse? TeacherCourse { get; set; }

    public ICollection<Homework>? Homeworks { get; set; }
}
