using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Banners;
using Eshop.RazorPage.Models.Categories;

namespace Eshop.RazorPage.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _client;

    public CategoryService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> CreateCategory(CreateCategoryCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Title), "Title");
        formData.Add(new StringContent(command.Slug), "Slug");

        formData.Add(new StringContent(command.SeoData.MetaTitle), "SeoData.MetaTitle");
        formData.Add(new StringContent(command.SeoData.MetaDescription), "SeoData.MetaDescription");
        formData.Add(new StringContent(command.SeoData.MetaKeyWords), "SeoData.MetaKeyWords");
        formData.Add(new StringContent(command.SeoData.IndexPage.ToString()), "SeoData.IndexPage");

        if (string.IsNullOrWhiteSpace(command.SeoData.Canonical) == false)
            formData.Add(new StringContent(command.SeoData.Canonical), "SeoData.Canonical");

        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);

        var result = await _client.PostAsync("category", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditCategory(EditCategoryCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Id.ToString()), "Id");
        formData.Add(new StringContent(command.Title), "Title");
        formData.Add(new StringContent(command.Slug), "Slug");

        formData.Add(new StringContent(command.SeoData.MetaTitle), "SeoData.MetaTitle");
        formData.Add(new StringContent(command.SeoData.MetaDescription), "SeoData.MetaDescription");
        formData.Add(new StringContent(command.SeoData.MetaKeyWords), "SeoData.MetaKeyWords");
        formData.Add(new StringContent(command.SeoData.IndexPage.ToString()), "SeoData.IndexPage");

        if (string.IsNullOrWhiteSpace(command.SeoData.Canonical) == false)
            formData.Add(new StringContent(command.SeoData.Canonical), "SeoData.Canonical");

        if (command.ImageFile != null)
            formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);

        var result = await _client.PutAsync("category", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteCategory(long categoryId)
    {
        var result = await _client.DeleteAsync($"category/{categoryId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> AddChild(AddChildCategoryCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Title), "Title");
        formData.Add(new StringContent(command.ParentId.ToString()), "ParentId");
        formData.Add(new StringContent(command.Slug), "Slug");

        formData.Add(new StringContent(command.SeoData.MetaTitle), "SeoData.MetaTitle");
        formData.Add(new StringContent(command.SeoData.MetaDescription), "SeoData.MetaDescription");
        formData.Add(new StringContent(command.SeoData.MetaKeyWords), "SeoData.MetaKeyWords");
        formData.Add(new StringContent(command.SeoData.IndexPage.ToString()), "SeoData.IndexPage");

        if (string.IsNullOrWhiteSpace(command.SeoData.Canonical) == false)
            formData.Add(new StringContent(command.SeoData.Canonical), "SeoData.Canonical");

        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);

        var result = await _client.PostAsync("category/AddChild", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<CategoryDto?> GetCategoryById(long categoryId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<CategoryDto>>($"category/{categoryId}");
        return result?.Data;
    }

    public async Task<List<CategoryDto>> GetCategories()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<CategoryDto>>>($"category");
        return result?.Data;
    }

    public async Task<List<ChildCategoryDto>> GetChild(long parentCategoryId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<List<ChildCategoryDto>>>($"category/getChild/{parentCategoryId}");
        return result?.Data;
    }
}