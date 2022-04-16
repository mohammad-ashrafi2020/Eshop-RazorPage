using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Roles;

namespace Eshop.RazorPage.Services.Roles;

public class RoleService : IRoleService
{
    private readonly HttpClient _client;
    private const string ModuleName = "role";
    public RoleService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> CreateRole(CreateRoleCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditRole(CreateRoleCommand command)
    {
        var result = await _client.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<RoleDto> GetRoleById(long roleId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<RoleDto?>>($"{ModuleName}/{roleId}");
        return result.Data;
    }

    public async Task<List<RoleDto>> GetRoles()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<RoleDto>>>(ModuleName);
        return result.Data;
    }
}