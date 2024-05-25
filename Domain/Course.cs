using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;
public class Course
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public int FacultyId { get; set; }

    // Navigation property for related faculty
    [ForeignKey("FacultyId")]
    public Faculty Faculty { get; set; }

    // Navigation property for TeacherCourseMap
    public ICollection<TeacherCourse> TeacherCourseMaps { get; set; }
}

