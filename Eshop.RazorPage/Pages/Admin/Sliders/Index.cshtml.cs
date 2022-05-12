using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Sliders;
using Eshop.RazorPage.Services.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Sliders
{
    public class IndexModel : BaseRazorPage
    {
        private readonly ISliderService _sliderService;

        public IndexModel(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public List<SliderDto> Sliders { get; set; }
        public async Task OnGet()
        {
            Sliders = await _sliderService.GetSliders();
        }

        public async Task<IActionResult> OnPostDeleteSlider(long sliderId)
        {
            return await AjaxTryCatch(() =>
            {
                return _sliderService.DeleteSlider(sliderId);
            });
        }
    }
}
