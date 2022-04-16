using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Sliders;

namespace Eshop.RazorPage.Services.Sliders;

public class SliderService : ISliderService
{
    private readonly HttpClient _client;
    private const string ModuleName = "slider";
    public SliderService(HttpClient client)
    {
        _client = client;
    }
    public async Task<ApiResult> CreateSlider(CreateSliderCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditSlider(EditSliderCommand command)
    {
        var result = await _client.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteSlider(long sliderId)
    {
        var result = await _client.DeleteAsync($"{ModuleName}/{sliderId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<SliderDto?> GetSliderById(long sliderId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<SliderDto?>>(ModuleName);
        return result.Data;
    }

    public async Task<List<SliderDto>> GetSliders()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<SliderDto>>>(ModuleName);
        return result.Data;
    }
}