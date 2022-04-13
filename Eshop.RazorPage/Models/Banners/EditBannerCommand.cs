namespace Eshop.RazorPage.Models.Banners;

public class EditBannerCommand
{
    public long Id { get; set; }
    public string Link { get; set; }
    public BannerPosition Position { get; set; }
    public IFormFile ImageFile { get; set; }
}