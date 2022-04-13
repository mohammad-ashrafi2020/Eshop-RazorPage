using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Banners;

namespace Eshop.RazorPage.Services.Banners;

public class BannerService : IBannerService
{
    private readonly HttpClient _client;

    public BannerService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> CreateBanner(CreateBannerCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Link), "Link");
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile");
        formData.Add(new StringContent(command.Position.ToString()), "Position");

        var result = await _client.PostAsync("banner", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditBanner(EditBannerCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Link), "Link");
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile");
        formData.Add(new StringContent(command.Position.ToString()), "Position");
        formData.Add(new StringContent(command.Id.ToString()), "Id");

        var result = await _client.PutAsync("banner", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteBanner(long bannerId)
    {
        var result = await _client.DeleteAsync($"banner/{bannerId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<BannerDto?> GetBannerById(long bannerId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<BannerDto>>($"banner/{bannerId}");
        return result?.Data;
    }

    public async Task<List<BannerDto>> GetList()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<BannerDto>>>($"banner");

        if (result?.Data == null)
            return new List<BannerDto>();

        return result?.Data;
    }
}