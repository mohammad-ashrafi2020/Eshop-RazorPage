using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Categories;
using Eshop.RazorPage.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Categories
{
    public class IndexModel : BaseRazorPage
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<CategoryDto> Categories { get; set; }
        public async Task OnGet()
        {
            Categories = await _categoryService.GetCategories();
        }

        public async Task<IActionResult> OnPostDelete(long id)
        {
            return await AjaxTryCatch(() => _categoryService.DeleteCategory(id));
        }
    }
}
