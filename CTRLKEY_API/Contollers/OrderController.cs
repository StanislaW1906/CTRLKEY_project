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

    // POST: api/order
    [HttpPost]
    public async Task<ActionResult> AddOrder([FromBody] OrdersDto dto)
    {
        if (dto.OrderDate == null)
        {
            dto.OrderDate = DateTime.Now;
        }
        var order = await _orderService.AddOrder(dto.UserId, dto.TotalPrice, dto.Status, dto.OrderDate.Value, dto.DeliveryAddress, dto.City, dto.NovaPoshtaAddress);
        return Ok(order);
    }

    // GET: api/order/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult> GetOrdersByUserId(int userId)
    {
        var orders = await _orderService.GetOrdersByUserId(userId);
        return Ok(orders);
    }

    // GET: api/order/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);
        return Ok(order);
    }

    // DELETE: api/order/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var order = await _orderService.DeleteOrderById(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok();
    }

    // GET: api/order
    [HttpGet]
    public async Task<ActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrders();
        return Ok(orders);
    }
}