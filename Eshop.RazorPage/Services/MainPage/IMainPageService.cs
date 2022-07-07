using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Services.Banners;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.Services.Sliders;

namespace Eshop.RazorPage.Services.MainPage;

public interface IMainPageService
{
    Task<MainPageDto> GetMainPageData();
}

class MainPageService : IMainPageService
{
    private ISliderService _sliderService;
    private IBannerService _bannerService;
    private IProductService _productService;

    public MainPageService(ISliderService sliderService, IBannerService bannerService, IProductService productService)
    {
        _sliderService = sliderService;
        _bannerService = bannerService;
        _productService = productService;
    }

    public async Task<MainPageDto> GetMainPageData()
    {
        var slider = await _sliderService.GetSliders();
        var banners = await _bannerService.GetList();

        var latestProductsResult = await _productService.GetProductForShop(new ProductShopFilterParam()
        {
            PageId = 1,
            Take = 8,
            SearchOrderBy = ProductSearchOrderBy.Latest,
            OnlyAvailableProducts = true
        });
        var latestProducts = latestProductsResult.Data;

        var specialProductsResult = await _productService.GetProductForShop(new ProductShopFilterParam()
        {
            PageId = 1,
            Take = 8,
            JustHasDiscount = true,
            OnlyAvailableProducts = true

        });
        var specialProducts = specialProductsResult.Data;

        var topVisitProductsResult = await _productService.GetProductForShop(new ProductShopFilterParam()
        {
            PageId = 1,
            Take = 8,
            OnlyAvailableProducts = true
        });
        var topVisitProducts = topVisitProductsResult.Data;


        return new MainPageDto()
        {
            Banners = banners,
            LatestProducts = latestProducts,
            Sliders = slider,
            SpectialProducts = specialProducts,
            TopVisitProducts = topVisitProducts
        };
    }
}