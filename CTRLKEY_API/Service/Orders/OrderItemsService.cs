using CTRLKEY_API.Data;

namespace CTRLKEY_API.Service.Orders;

public class OrderItemsService
{
    private readonly DBContext _database;

    public OrderItemsService(DBContext dbContext)
    {
        _database = dbContext;
    }
    
    //
    public async Task<OrderItems> AddOrderItem(int orderId, int productId, int quantity, double unitPrice)
    {
        var orderItems = new OrderItems
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
        
        _database.OrderItems.Add(orderItems);
        await _database.SaveChangesAsync();
        return orderItems;   
    }
    
    //
    public async Task<OrderItems> DeleteOrderItemById(int id)
    {
        var orderItems = await _database.OrderItems.FindAsync(id);
        if (orderItems == null)
        {
            return null;
        }
        
        _database.OrderItems.Remove(orderItems);
        await _database.SaveChangesAsync();
        return orderItems; 
    }
    
    //
    public async Task<OrderItems> GetOrderItemById(int id)
    {
        var orderItem = await _database.OrderItems.FindAsync(id);
        return orderItem;   
    }
    
    //
    public async Task<OrderItems> UpdateOrderItem(int id, OrderItems orderItems)
    {
        var orderItem = await _database.OrderItems.FindAsync(id);
        if (orderItem == null)
        {
            return null;
        }
        orderItem.OrderId = orderItems.OrderId;
        orderItem.ProductId = orderItems.ProductId;
        orderItem.Quantity = orderItems.Quantity;
        orderItem.UnitPrice = orderItems.UnitPrice;
        
        _database.OrderItems.Update(orderItem);
        await _database.SaveChangesAsync();
        return orderItem;
    }
}