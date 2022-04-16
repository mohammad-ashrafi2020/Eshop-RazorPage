namespace Eshop.RazorPage.Models.Roles;

public class CreateRoleCommand
{
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}