using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Services.Auth;
using Eshop.RazorPage.Services.Banners;
using Eshop.RazorPage.Services.Categories;
using Eshop.RazorPage.Services.Comments;
using Eshop.RazorPage.Services.Orders;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.Services.Roles;
using Eshop.RazorPage.Services.Sellers;
using Eshop.RazorPage.Services.Sliders;
using Eshop.RazorPage.Services.UserAddress;
using Eshop.RazorPage.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();


builder.Services.RegisterApiServices();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.Run();
