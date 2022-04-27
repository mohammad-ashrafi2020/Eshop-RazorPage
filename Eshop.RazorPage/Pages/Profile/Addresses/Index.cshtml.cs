using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.UserAddress;
using Eshop.RazorPage.Services.UserAddress;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Profile.Addresses
{
    public class IndexModel : BaseRazorPage
    {
        private readonly IUserAddressService _userAddress;
        private readonly IRenderViewToString _renderViewToString;
        public IndexModel(IUserAddressService userAddress, IRenderViewToString renderViewToString)
        {
            _userAddress = userAddress;
            _renderViewToString = renderViewToString;
        }

        public List<AddressDto> Addresses { get; set; }
        public async Task OnGet()
        {
            Addresses = await _userAddress.GetUserAddresses();
        }

        public async Task<IActionResult> OnGetShowAddPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Add", new CreateUserAddressCommand(),
                    PageContext);

                return ApiResult<string>.Success(view);
            });
        }
    }
}
