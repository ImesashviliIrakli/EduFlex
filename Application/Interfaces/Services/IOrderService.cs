using Application.Models.Dtos.OrderDtos;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDetailsDto> GetOrderDetailsAsync(int orderId);
    Task<string> CreateOrderAndGetUrlAsync();
    Task UpdateOrderAsync(UpdateOrderDto updateOrderDto);
    Task DeleteOrderAsync(int orderId);
}
