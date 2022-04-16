namespace Eshop.RazorPage.Models.Roles;

public class RoleDto : BaseDto
{
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}

public enum Permission
{
    PanelAdmin,
    EditProfile,
    ChangePassword,
    CRUD_Banner,
    CRUD_Slider,
    CURD_User,
    CRUD_Product,
    Seller_Management,
    Order_Management,
    Role_Management,
    Comment_Management,
    Category_Management,
    Add_Inventory,
    Edit_Inventory,
    User_Management,
    Seller_Panel
}