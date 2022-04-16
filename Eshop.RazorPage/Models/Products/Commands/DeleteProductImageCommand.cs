namespace Eshop.RazorPage.Models.Products.Commands;

public class DeleteProductImageCommand
{
    public long ImageId { get; set; }
    public long ProductId { get; set; }
}