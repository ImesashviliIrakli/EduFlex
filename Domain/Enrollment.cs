using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Enrollment
{
    public int Id { get; set; }
    public int StudentUserId { get; set; }
    public int TeacherCourseMapId { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; }
    // Navigation properties
    [ForeignKey("StudentUserId")]
    public required Student Student { get; set; }

    [ForeignKey("TeacherCourseMapId")]
    public required TeacherCourse TeacherCourseMap { get; set; }
}

