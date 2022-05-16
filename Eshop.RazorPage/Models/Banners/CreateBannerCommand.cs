using System.ComponentModel.DataAnnotations;
using Common.Application.Validation.CustomValidation.IFormFile;

namespace Eshop.RazorPage.Models.Banners;

public class CreateBannerCommand
{
    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url)]
    public string Link { get; set; }
    public BannerPosition Position { get; set; }

    [Display(Name = "عکس")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile ImageFile { get; set; }
}