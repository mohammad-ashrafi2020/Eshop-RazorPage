using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Infrastructure.Utils;
using Eshop.RazorPage.Models.Roles;
using Eshop.RazorPage.Services.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Roles;

[BindProperties]
public class AddModel : BaseRazorPage
{
    private readonly IRoleService _roleService;

    public AddModel(IRoleService roleService)
    {
        _roleService = roleService;
    }
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(string[] permission)
    {
        var permissionModel = new List<Permission>();
        foreach (var item in permission)
        {
            try
            {
                permissionModel.Add(EnumUtils.ParseEnum<Permission>(item));
            }
            catch
            {
               // 
            }
        }
        var result = await _roleService.CreateRole(new CreateRoleCommand()
        {
            Title = Title,
            Permissions = permissionModel
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}