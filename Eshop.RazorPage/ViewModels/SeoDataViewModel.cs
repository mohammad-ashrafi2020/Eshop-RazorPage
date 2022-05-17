using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Models;

namespace Eshop.RazorPage.ViewModels;

public class SeoDataViewModel
{
    [Display(Name = "عنوان صفحه")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string MetaTitle { get; set; }

    [DataType(DataType.MultilineText)] public string? MetaDescription { get; set; } = "";

    public string? MetaKeyWords { get; set; } = "";
    public bool IndexPage { get; set; }

    [DataType(DataType.Url)] public string? Canonical { get; set; } = "";

    [DataType(DataType.MultilineText)] public string? Schema { get; set; } = "";

    public SeoData MapToSeoData()
    {
        return new SeoData()
        {
            Canonical = Canonical,
            IndexPage = IndexPage,
            MetaDescription = MetaDescription,
            MetaKeyWords = MetaKeyWords,
            MetaTitle = MetaTitle,
            Schema = Schema
        };
    }

    public static SeoDataViewModel MapSeoDataToViewModel(SeoData seoData)
    {
        return new SeoDataViewModel()
        {
            Canonical = seoData.Canonical,
            IndexPage = seoData.IndexPage,
            MetaDescription = seoData.MetaDescription,
            MetaKeyWords = seoData.MetaKeyWords,
            MetaTitle = seoData.MetaTitle,
            Schema = seoData.Schema
        };
    }
}