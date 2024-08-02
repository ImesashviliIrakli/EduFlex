using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;
public class Course
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public string Description { get; set; }
    public int Price { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    public int FacultyId { get; set; }

    // Navigation property for related faculty
    [ForeignKey("FacultyId")]
    public Faculty? Faculty { get; set; }
}

