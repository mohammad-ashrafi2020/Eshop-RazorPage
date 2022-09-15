using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Infrastructure.Utils;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Orders
{
    public class IndexModel : BaseRazorFilter<OrderFilterParams>
    {
        private IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderFilterResult FilterResult { get; set; }
        public async Task OnGet(string? startDate, string? endDate)
        {
            if (string.IsNullOrWhiteSpace(startDate) == false)
                FilterParams.StartDate = startDate.ToMiladi();


            if (string.IsNullOrWhiteSpace(endDate) == false)
                FilterParams.StartDate = endDate.ToMiladi();

            FilterParams.Take = 1;
            FilterResult = await _orderService.GetOrders(FilterParams);
        }
    }
}
