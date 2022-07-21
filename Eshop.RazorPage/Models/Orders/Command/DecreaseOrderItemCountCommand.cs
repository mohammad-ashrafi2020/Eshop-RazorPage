namespace Eshop.RazorPage.Models.Orders.Command;

public class DecreaseOrderItemCountCommand
{
    public long UserId { get; set; }
    public long ItemId { get; set; }
    public int Count { get; set; }
}