using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Order
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string StudentUserId { get; set; }
    [Required]
    public int TeacherCourseId { get; set; }
    [Required]
    public decimal Amount { get; set; }

    [ForeignKey("TeacherCourseId")]
    public TeacherCourse? TeacherCourse { get; set; }
}
    