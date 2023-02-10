using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Categories;
using Eshop.RazorPage.Services.Categories;
using Eshop.RazorPage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Admin.Categories;

[BindProperties]
public class AddModel : BaseRazorPage
{
    private readonly ICategoryService _categoryService;

    public AddModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "تصویر دسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public IFormFile ImageFile { get; set; }

    public SeoDataViewModel SeoData { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(long? parentId)
    {
        if (parentId == null)
        {
            var result = await _categoryService.CreateCategory(new CreateCategoryCommand()
            {
                Title = Title,
                Slug = Slug,
                SeoData = SeoData.MapToSeoData(),
                ImageFile = ImageFile
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));

        }


        var res = await _categoryService.AddChild(new AddChildCategoryCommand()
        {
            ParentId = (long)parentId,
            Title = Title,
            SeoData = SeoData.MapToSeoData(),
            Slug = Slug,
            ImageFile = ImageFile
        });
        return RedirectAndShowAlert(res, RedirectToPage("Index"));

    }
}