namespace CTRLKEY_API.Models.DTOs;

public class OrdersDto
{
    public int UserId { get; set; }
    public double? TotalPrice { get; set; }
    public string? Status { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? City { get; set; }
    public string? NovaPoshtaAddress { get; set; }
}