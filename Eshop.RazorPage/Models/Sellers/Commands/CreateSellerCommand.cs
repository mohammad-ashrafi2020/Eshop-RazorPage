namespace Eshop.RazorPage.Models.Sellers.Commands;

public class CreateSellerCommand
{
    public long UserId { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
}