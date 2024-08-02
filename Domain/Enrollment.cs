using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Enrollment
{
    public int Id { get; set; }
    public int StudentUserId { get; set; }
    public int TeacherCourseId { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; }

    [ForeignKey("TeacherCourseId")]
    public required TeacherCourse TeacherCourseMap { get; set; }
}

