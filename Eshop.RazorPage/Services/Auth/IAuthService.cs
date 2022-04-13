using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Auth;

namespace Eshop.RazorPage.Services.Auth;

public interface IAuthService
{
    Task<ApiResult<LoginResponse>?> Login(LoginCommand command);
    Task<ApiResult?> Register(RegisterCommand command);
    Task<ApiResult<LoginResponse>?> RefreshToken();
    Task<ApiResult?> Logout();
}