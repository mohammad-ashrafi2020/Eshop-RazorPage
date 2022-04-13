namespace Eshop.RazorPage.Models.Orders.Command;

public class DecreaseOrderItemCountCommand
{
    public long UserId { get; set; }
    public long OrderItemId { get; set; }
    public int Count { get; set; }
}