using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Profile.Orders
{
    public class ShowModel : PageModel
    {
        private IOrderService _orderService;

        public ShowModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderDto Order { get; set; }
        public async Task<IActionResult> OnGet(long id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null || order.UserId != User.GetUserId())
                return RedirectToPage("Index");

            Order = order;
            return Page();
        }
    }
}
