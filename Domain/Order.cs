using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Order
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required int StudentId { get; set; }
    [Required]
    public int TeacherCourseId { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public int Status { get; set; }


    [ForeignKey("StudentId")]
    public Student? Student { get; set; }

    [ForeignKey("TeacherCourseId")]
    public TeacherCourse? TeacherCourse { get; set; }
}
    