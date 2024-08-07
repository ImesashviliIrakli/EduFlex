using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.OrderDtos;

public class AddOrderDto
{
    public int StudentId { get; set; }
    [Required]
    public int TeacherCourseId { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [JsonIgnore]
    public int Status { get; set; } = 0;
}