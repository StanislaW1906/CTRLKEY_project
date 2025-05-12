using CTRLKEY_API.Models.Users;
using CTRLKEY_API.Service.Orders;

namespace CTRLKEY_API.Models.Orders;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public double? TotalPrice { get; set; }
    public string? Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? City { get; set; }
    public string? NovaPoshtaAddress { get; set; }

}