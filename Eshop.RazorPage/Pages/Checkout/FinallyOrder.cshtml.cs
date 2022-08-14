using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Checkout
{
    public class FinallyOrderModel : PageModel
    {
        private IOrderService _orderService;

        public FinallyOrderModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderDto OrderDto { get; set; }
        public async Task<IActionResult> OnGet(long orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null || order.UserId != User.GetUserId())
                return Redirect("/");

            OrderDto = order;
            return Page();
        }
    }
}
