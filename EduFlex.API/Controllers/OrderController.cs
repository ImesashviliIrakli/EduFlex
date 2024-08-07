using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduFlex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : BaseController
{

    #region Injection
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    #endregion
    [HttpPost]
    public async Task<IActionResult> GetPaymentUrl()
    {
        var data = await _orderService.CreateOrderAndGetUrlAsync();
        return CreateResponse(data);
    }
}
