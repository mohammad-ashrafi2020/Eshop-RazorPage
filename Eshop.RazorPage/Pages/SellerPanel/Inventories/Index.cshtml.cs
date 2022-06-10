using Eshop.RazorPage.Models.Sellers;
using Eshop.RazorPage.Services.Sellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.SellerPanel.Inventories;

public class IndexModel : PageModel
{
    private ISellerService _sellerService;

    public IndexModel(ISellerService sellerService)
    {
        _sellerService = sellerService;
    }

    public List<InventoryDto> Inventories { get; set; }
    public async Task<IActionResult> OnGet()
    {
        var seller = await _sellerService.GetCurrentSeller();
        if (seller == null)
            return Redirect("/");

        Inventories = await _sellerService.GetSellerInventories();
        return Page();
    }
}