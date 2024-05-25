using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class TeacherCourse
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }

    // Navigation properties
    [ForeignKey("CourseId")]
    public Course Course { get; set; }

    [ForeignKey("TeacherId")]
    public Teacher Teacher { get; set; }

    // Navigation property for enrollments
    public ICollection<Enrollment> Enrollments { get; set; }
}
