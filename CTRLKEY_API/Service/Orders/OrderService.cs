using CTRLKEY_API.Data;
using CTRLKEY_API.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace CTRLKEY_API.Service.Orders;

public class OrdersService
{
    private readonly DBContext _database;
    
    public OrdersService(DBContext dbContext)
    {
        _database = dbContext;
    }
    
    //
    public async Task<Order> AddOrder(int userId, double totalPrice, string status, DateTime orderDate, string deliveryAddress, string city, string novaPoshtaAddress)
    {
        var order = new Order
        {
            UserId = userId,
            TotalPrice = totalPrice,
            Status = status,
            OrderDate = orderDate,
            DeliveryAddress = deliveryAddress,
            City = city,
            NovaPoshtaAddress = novaPoshtaAddress
        };
        
        _database.Orders.Add(order);
        await _database.SaveChangesAsync();
        return order;   
    }
    
    //
    public async Task<Order> DeleteOrderById(int id)
    {
        var order = await _database.Orders.FindAsync(id);
        if (order == null)
        {
            return null;
        }
        
        _database.Orders.Remove(order);
        await _database.SaveChangesAsync();
        return order;  
    }
    
    //
    public async Task<Order> GetOrderById(int id)
    {
        var order = await _database.Orders.FirstOrDefaultAsync(o => o.Id == id);
        return order;  
    }
    
    //
    public async Task<List<Order>> GetOrdersByUserId(int userId)
    {
        var orders = await _database.Orders
            .Where(o => o.UserId == userId)
            .ToListAsync();
        return orders;   
    }
    
    //
    public async Task<List<Order>> GetAllOrders()
    {
        var orders = await _database.Orders.ToListAsync();
        return orders; 
    }
    
}