using System.Text;
using System.Text.Json.Serialization;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Models.Products.Commands;
using Newtonsoft.Json;

namespace Eshop.RazorPage.Services.Products;

public class ProductService : IProductService
{
    private readonly HttpClient _client;
    private const string ModuleName = "product";
    public ProductService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> CreateProduct(CreateProductCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditProduct(EditProductCommand command)
    {
        var result = await _client.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> AddImage(AddProductImageCommand command)
    {
        var result = await _client.PostAsJsonAsync($"{ModuleName}/images", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteProductImage(DeleteProductImageCommand command)
    {
        var json = JsonConvert.SerializeObject(command);
        var message = new HttpRequestMessage(HttpMethod.Delete, $"{ModuleName}/images")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var result = await _client.SendAsync(message);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ProductDto?> GetProductById(long productId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<ProductDto?>>($"{ModuleName}/{productId}");
        return result?.Data;
    }

    public async Task<ProductDto?> GetProductBySlug(string slug)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<ProductDto?>>($"{ModuleName}/bySlug/{slug}");
        return result?.Data;
    }

    public async Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams)
    {
        var url = $"{ModuleName}?pageId={filterParams.PageId}&take={filterParams.Take}" +
            $"&slug={filterParams.Slug}&slug={filterParams.Title}";
        if (filterParams.Id != null)
            url += $"&Id={filterParams.Id}";
        var result = await _client.GetFromJsonAsync<ApiResult<ProductFilterResult>>(url);
        return result?.Data;
    }

    public async Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParams)
    {
        var url = $"{ModuleName}?pageId={filterParams.PageId}&take={filterParams.Take}" +
                  $"&categorySlug={filterParams.CategorySlug}&onlyAvailableProducts={filterParams.OnlyAvailableProducts}" +
                  $"&search={filterParams.Search}&SearchOrderBy={filterParams.SearchOrderBy}&JustHasDiscount={filterParams.JustHasDiscount}";
      
        var result = await _client.GetFromJsonAsync<ApiResult<ProductShopResult>>(url);
        return result?.Data;
    }
}