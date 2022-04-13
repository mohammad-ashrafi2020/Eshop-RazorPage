namespace Eshop.RazorPage.Models.Orders;

public class OrderDto : BaseDto
{
    public long UserId { get; set; }
    public string UserFullName { get; set; }
    public OrderStatus Status { get; set; }
    public OrderDiscount? Discount { get; set; }
    public OrderAddress? Address { get; set; }
    public ShippingMethod? ShippingMethod { get; set; }
    public List<OrderItemDto> Items { get; set; }
    public DateTime? LastUpdate { get; set; }
}