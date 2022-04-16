namespace Eshop.RazorPage.Models.Sellers.Commands;

public class EditSellerInventoryCommand
{
    public long SellerId { get; set; }
    public long InventoryId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int? DiscountPercentage { get; set; }
}