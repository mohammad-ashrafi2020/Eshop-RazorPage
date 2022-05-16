using System.ComponentModel.DataAnnotations;
using Common.Application.Validation.CustomValidation.IFormFile;

namespace Eshop.RazorPage.Models.Banners;

public class EditBannerCommand
{
    public long Id { get; set; }
    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url)]
    public string Link { get; set; }
    public BannerPosition Position { get; set; }

    [Display(Name = "عکس")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile? ImageFile { get; set; }
}