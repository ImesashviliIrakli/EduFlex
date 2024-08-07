using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.StripeService;
using Application.Models.Dtos.OrderDtos;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IStripeService _stripeService;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;
    #region Injection
    public OrderService(
        IOrderRepository orderRepository,
        IStripeService stripeService,
        IMapper mapper,
        ILogger<OrderService> logger
        )
    {
        _orderRepository = orderRepository;
        _stripeService = stripeService;
        _mapper = mapper;
        _logger = logger;
    }
    #endregion
    public async Task<string> CreateOrderAndGetUrlAsync()
    {
        return await _stripeService.GetStripePaymentUrl(100, "TestCourse", "Card");
    }

    public Task DeleteOrderAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetailsDto> GetOrderDetailsAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
    {
        throw new NotImplementedException();
    }
}
