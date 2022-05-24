using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Products
{
    public class IndexModel : BaseRazorFilter<ProductFilterParams>
    {
        private IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public ProductFilterResult FilterResult { get; set; }
        public async Task OnGet()
        {
            FilterResult = await _productService.GetProductByFilter(FilterParams);
        }
    }
}
