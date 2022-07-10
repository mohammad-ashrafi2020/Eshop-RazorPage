using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Models.Sellers;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.Services.Sellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages;

public class ProductModel : PageModel
{
    private readonly IProductService _service;
    private readonly ISellerService _sellerService;
    public ProductModel(IProductService service, ISellerService sellerService)
    {
        _service = service;
        _sellerService = sellerService;
    }

    public SingleProductDto ProductPageModel { get; set; }
    public async Task<IActionResult> OnGet(string slug)
    {
        var product = await _service.GetSingleProduct(slug);
        if (product == null)
            return NotFound();

        ProductPageModel = product;
        return Page();
    }
}