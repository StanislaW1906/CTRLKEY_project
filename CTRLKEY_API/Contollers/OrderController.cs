using CTRLKEY_API.Models.DTOs;
using CTRLKEY_API.Service.Orders;
using Microsoft.AspNetCore.Mvc;

namespace CTRLKEY_API.Contollers;


[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    
    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }
    
    //
    [HttpPost("add-order")]
    public async Task<ActionResult> AddOrder([FromBody] OrdersDto dto)
    {
        if (dto.OrderDate == null)
        {
            dto.OrderDate = DateTime.Now;
            
        }
        var order = await _orderService.AddOrder(dto.UserId, dto.TotalPrice, dto.Status, dto.OrderDate.Value , dto.DeliveryAddress, dto.City, dto.NovaPoshtaAddress);
        return Ok(order);
    }
    
    //
    [HttpGet("get-orders-user/{userId}")]
    public async Task<ActionResult> GetOrdersByUserId(int userId)
    {
        var orders = await _orderService.GetOrdersByUserId(userId);
        return Ok(orders);
    }
    
    //
    [HttpGet("get-orders/{id}")]
    public async Task<ActionResult> GetOrdersById(int id)
    {
        var orders = await _orderService.GetOrderById(id);
        return Ok(orders);
    }
    
    //
    [HttpDelete("delete-order/{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var order = await _orderService.DeleteOrderById(id);
        return Ok();
    }
    
    //
    [HttpDelete("get-all-orders")]
    public async Task<ActionResult> GetAllOrders()
    {
        var order = await _orderService.GetAllOrders();
        return Ok();
    }
}