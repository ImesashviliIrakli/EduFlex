using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Enrollment
{
    [Key]
    public int Id { get; set; }
    public int StudentUserId { get; set; }
    public int OrderId { get; set; }
    public DateTime Date { get; set; }
    public int Status { get; set; }

    [ForeignKey("OrderId")]
    public Order? Order { get; set; }
}