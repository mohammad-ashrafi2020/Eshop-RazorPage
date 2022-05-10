using System.ComponentModel.DataAnnotations;
using Common.Application.Validation.CustomValidation.IFormFile;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Sliders;
using Eshop.RazorPage.Services.Sliders;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Admin.Sliders
{
    [BindProperties]
    public class AddModel : BaseRazorPage
    {
        private readonly ISliderService _sliderService;

        public AddModel(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Url)]
        public string Link { get; set; }

        [Display(Name = "عکس اسلایدر")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [FileImage(ErrorMessage = "عکس نامعتبر است")]
        public IFormFile ImageFile { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _sliderService.CreateSlider(new CreateSliderCommand()
            {
                ImageFile = ImageFile,
                Link = Link,
                Title = Title
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
