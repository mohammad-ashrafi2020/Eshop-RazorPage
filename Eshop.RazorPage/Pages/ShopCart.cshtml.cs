using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Models.Orders.Command;
using Eshop.RazorPage.Models.Users;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages
{
    public class ShopCartModel : BaseRazorPage
    {
        private readonly IOrderService _orderService;

        public ShopCartModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderDto? OrderDto { get; set; }
        public async Task OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                OrderDto = await _orderService.GetCurrentOrder();
            }
            else
            {

            }
        }

        public async Task<IActionResult> OnPostDeleteItem(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(() => _orderService.DeleteOrderItem(new DeleteOrderItemCommand()
                {
                    OrderItemId = id
                }));
            }
            else
            {
                return Page();
            }


        }

        public async Task<IActionResult> OnPostAddItem(long inventoryId, int count)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(() => _orderService.AddOrderItem(new AddOrderItemCommand()
                {
                    UserId = User.GetUserId(),
                    InventoryId = inventoryId,
                    Count = count
                }));
            }
            else
            {
                return Page();
            }
        }
    }
}
