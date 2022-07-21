namespace Eshop.RazorPage.Models.Orders.Command;

public class IncreaseOrderItemCountCommand
{
    public long UserId { get; set; }
    public long ItemId { get; set; }
    public int Count { get; set; }
}