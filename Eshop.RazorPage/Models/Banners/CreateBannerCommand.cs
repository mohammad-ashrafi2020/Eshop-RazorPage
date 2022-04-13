namespace Eshop.RazorPage.Models.Banners;

public class CreateBannerCommand
{
    public string Link { get; set; }
    public BannerPosition Position { get; set; }
    public IFormFile ImageFile { get; set; }
}