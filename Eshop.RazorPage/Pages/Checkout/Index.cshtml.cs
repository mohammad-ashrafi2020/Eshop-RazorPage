using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Models.ShippingMethods;
using Eshop.RazorPage.Models.UserAddress;
using Eshop.RazorPage.Services.Orders;
using Eshop.RazorPage.Services.ShippingMethods;
using Eshop.RazorPage.Services.UserAddress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;
        private IUserAddressService _userAddressService;
        private IShippingMethodService _shippingMethodService;
        public IndexModel(IOrderService orderService, IUserAddressService userAddressService, IShippingMethodService shippingMethodService)
        {
            _orderService = orderService;
            _userAddressService = userAddressService;
            _shippingMethodService = shippingMethodService;
        }

        public List<AddressDto> Addresses { get; set; }
        public OrderDto Order { get; set; }
        public List<ShippingMethodDto> ShippingMethods { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var order = await _orderService.GetCurrentOrder();
            if (order == null)
                return RedirectToPage("../Index");

            Order = order;
            Addresses = await _userAddressService.GetUserAddresses();
            ShippingMethods = await _shippingMethodService.GetShippingMethods();
            if (ShippingMethods.Any() == false)
                return RedirectToPage("../Index");

            return Page();
        }
    }
}
