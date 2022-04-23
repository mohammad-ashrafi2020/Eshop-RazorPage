using System.Net;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Auth;

namespace Eshop.RazorPage.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _accessor;
    public AuthService(HttpClient client, IHttpContextAccessor accessor)
    {
        _client = client;
        _accessor = accessor;
    }

    public async Task<ApiResult<LoginResponse>?> Login(LoginCommand command)
    {
        var result = await _client.PostAsJsonAsync("auth/login", command);
        return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
    }

    public async Task<ApiResult?> Register(RegisterCommand command)
    {
        var result = await _client.PostAsJsonAsync("auth/register", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult<LoginResponse>?> RefreshToken()
    {
        var refreshToken = _accessor.HttpContext.Request.Cookies["refreshToken"];
        var result = await _client.PostAsync($"auth/refreshToken?refreshToken={refreshToken}",null);
        return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
    }

    public async Task<ApiResult?> Logout()
    {
        try
        {
            var result = await _client.DeleteAsync("auth/logout");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }
        catch (Exception e)
        {
            return ApiResult.Error();
        }
    }
}