using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Users;
using Eshop.RazorPage.Models.Users.Commands;

namespace Eshop.RazorPage.Services.Users;

public interface IUserService
{
    Task<ApiResult> CreateUser(CreateUserCommand command);
    Task<ApiResult> EditUser(EditUserCommand command);
    Task<ApiResult> EditUserCurrent(EditUserCommand command);
    Task<ApiResult> ChangePassword(ChangePasswordCommand command);

    Task<UserDto?> GetUserById(long userId);
    Task<UserDto?> GetCurrentUser();
    Task<UserFilterResult> GetUsersByFilter(UserFilterParams filterParams);
}