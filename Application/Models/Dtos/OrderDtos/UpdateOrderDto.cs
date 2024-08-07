using System.ComponentModel.DataAnnotations;
using Application.Enums;

namespace Application.Models.Dtos.OrderDtos;

public class UpdateOrderDto
{
    [Required]
    public int Id { get; set; }
    public OrderStatus Status { get; set; } 
}