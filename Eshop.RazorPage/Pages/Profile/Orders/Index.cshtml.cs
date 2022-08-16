using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Profile.Orders
{
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderFilterResult FilterResult { get; set; }
        public async Task OnGet(int pageId=1,OrderStatus? status=null)
        {
            FilterResult = await _orderService.GetUserOrders(pageId, 10, status);
        }
    }
}
