using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Banners;
using Eshop.RazorPage.Services.Banners;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Banners;

public class IndexModel : BaseRazorPage
{
    private readonly IBannerService _bannerService;
    private readonly IRenderViewToString _renderView;
    public IndexModel(IBannerService bannerService, IRenderViewToString renderView)
    {
        _bannerService = bannerService;
        _renderView = renderView;
    }

    public List<BannerDto> Banners { get; set; }
    public async Task OnGet()
    {
        Banners = await _bannerService.GetList();
    }

    public async Task<IActionResult> OnGetRenderAddPage()
    {
        return await AjaxTryCatch(async () =>
        {
            var view = await _renderView.RenderToStringAsync("_Add", new CreateBannerCommand(), PageContext);
            return ApiResult<string>.Success(view);
        });
    }
    public async Task<IActionResult> OnGetRenderEditPage(long id)
    {
        return await AjaxTryCatch(async () =>
        {
            var banner = await _bannerService.GetBannerById(id);
            if (banner == null)
                return ApiResult<string>.Error();

            var model = new EditBannerCommand()
            {
                Id = id,
                Link = banner.Link,
                Position = banner.Position
            };
            var view = await _renderView.RenderToStringAsync("_Edit", model, PageContext);
            return ApiResult<string>.Success(view);
        });
    }


    public async Task<IActionResult> OnPostDelete(long id)
    {
        return await AjaxTryCatch(() =>
            _bannerService.DeleteBanner(id));
    }
    public async Task<IActionResult> OnPostEditBanner(EditBannerCommand command)
    {
        return await AjaxTryCatch(() =>
            _bannerService.EditBanner(command));
    }
    public async Task<IActionResult> OnPostCreateBanner(CreateBannerCommand command)
    {
        return await AjaxTryCatch(() =>
            _bannerService.CreateBanner(command));
    }
}