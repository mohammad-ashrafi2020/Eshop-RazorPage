using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Roles;

namespace Eshop.RazorPage.Services.Roles;

public interface IRoleService
{
    Task<ApiResult> CreateRole(CreateRoleCommand command);
    Task<ApiResult> EditRole(EditRoleCommand command);
    Task<RoleDto> GetRoleById(long roleId);
    Task<List<RoleDto>> GetRoles();
}