using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.OrderDtos;

public class UpdateOrderDto
{
    [Required]
    public int Id { get; set; }
    public int Status { get; set; }
}