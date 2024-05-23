using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;
public class Course
{
	[Key]
	public int Id { get; set; }
	[Required]
	public string Title { get; set; }

	[Required]
	public int FacultyId { get; set; }
	[ForeignKey(nameof(FacultyId))]
	public Faculty Faculty { get; set; }

	[Required]
	public int TeacherId { get; set; }
	[ForeignKey(nameof(TeacherId))]
	public Teacher Teacher { get; set; }
}
