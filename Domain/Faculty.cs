using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;
public class Faculty
{
	[Key]
	public int Id { get; set; }
	[Required]
	public required string Name { get; set; }
	public ICollection<Course> Courses { get; set; } = new List<Course>();
}
