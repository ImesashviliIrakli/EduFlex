using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly AppDBContext _context;
    #region Injection
    public OrderRepository(AppDBContext context)
    {
        _context = context;
    }
    #endregion

    #region Read
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders.Include(x => x.Student).FirstOrDefaultAsync(x => x.Id.Equals(orderId));
    }
    #endregion

    #region Write
    public async Task CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
    #endregion
}
