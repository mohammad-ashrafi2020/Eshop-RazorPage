using Common.Application.Validation.CustomValidation.IFormFile;
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
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Title), "Title");
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
        formData.Add(new StringContent(command.Link), "Link");

        var result = await _client.PostAsync($"{ModuleName}", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditSlider(EditSliderCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Title), "Title");

        if (command.ImageFile != null && command.ImageFile.IsImage())
            formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
        formData.Add(new StringContent(command.Link), "Link");
        formData.Add(new StringContent(command.Id.ToString()), "Id");

        var result = await _client.PutAsync($"{ModuleName}", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteSlider(long sliderId)
    {
        var result = await _client.DeleteAsync($"{ModuleName}/{sliderId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<SliderDto?> GetSliderById(long sliderId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<SliderDto?>>($"{ModuleName}/{sliderId}");
        return result.Data;
    }

    public async Task<List<SliderDto>> GetSliders()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<SliderDto>>>(ModuleName);
        return result.Data;
    }
}