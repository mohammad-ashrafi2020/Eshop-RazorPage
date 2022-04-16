namespace Eshop.RazorPage.Models.Sellers.Commands;

public class AddSellerInventoryCommand
{
    public long SellerId { get; set;}
    public long ProductId { get; set;}
    public int Count { get; set;}
    public int Price { get; set;}
    public int? PercentageDiscount { get; set;}
}