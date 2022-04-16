namespace Eshop.RazorPage.Models.Products.Commands;

public class AddProductImageCommand
{
    public IFormFile ImageFile { get; set; }
    public long ProductId { get; set; }
    public int Sequence { get; set; }
}