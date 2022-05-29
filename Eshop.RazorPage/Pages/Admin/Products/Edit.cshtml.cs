using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Common.Application.Validation.CustomValidation.IFormFile;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Models.Products.Commands;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Products;

[BindProperties]
public class EditModel : BaseRazorPage
{
    private IProductService _productService;

    public EditModel(IProductService productService)
    {
        _productService = productService;
    }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "عکس محصول")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [UIHint("Ckeditor4")]
    public string Description { get; set; }

    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Range(1, long.MaxValue, ErrorMessage = "دسته بندی را وارد کنید")]
    public long CategoryId { get; set; }

    [Display(Name = "زیردسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Range(1, long.MaxValue, ErrorMessage = "زیر دسته بندی را وارد کنید")]
    public long SubCategoryId { get; set; }

    [Display(Name = "دسته بندی سوم")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public long? SecondarySubCategoryId { get; set; }

    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }


    public SeoDataViewModel SeoData { get; set; }

    public List<string> Keys { get; set; } = new();
    public List<string> Values { get; set; } = new();
    public async Task<IActionResult> OnGet(long productId)
    {
        var product = await _productService.GetProductById(productId);
        if (product == null)
            return RedirectToPage("Index");

        Title = product.Title;
        Slug = product.Slug;
        Description = product.Description;
        SeoData=SeoDataViewModel.MapSeoDataToViewModel(product.SeoData);
        CategoryId = product.Category.Id;
        SubCategoryId = product.SubCategory.Id;
        SecondarySubCategoryId = product.SecondarySubCategory?.Id;
        InitSpecifications(product.Specifications);
        return Page();
    }

    public async Task<IActionResult> OnPost(long productId)
    {
        var result = await _productService.EditProduct(new EditProductCommand()
        {
            Title = Title,
            SeoData = SeoData.MapToSeoData(),
            Slug = Slug,
            ImageFile = ImageFile,
            SecondarySubCategoryId = SecondarySubCategoryId,
            CategoryId = CategoryId,
            SubCategoryId = SubCategoryId,
            Description = Description,
            ProductId = productId,
            Specifications = ConvertSpecifications()
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }

    public void InitSpecifications(List<ProductSpecificationDto> specifications)
    {
        foreach (var specification in specifications)
        {
            Keys.Add(specification.Key);
            Values.Add(specification.Value);
        }
    }
    private Dictionary<string, string> ConvertSpecifications()
    {
        var specifications = new Dictionary<string, string>();
        Keys.RemoveAll(r => r == null || string.IsNullOrWhiteSpace(r));
        Values.RemoveAll(r => r == null || string.IsNullOrWhiteSpace(r));
        for (var i = 0; i < Keys.Count; i++)
        {
            specifications.Add(Keys[i], Values[i]);
        }

        return specifications;
    }
}