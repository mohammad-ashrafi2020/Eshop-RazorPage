using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Models.Auth;
using Eshop.RazorPage.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        private IAuthService _authService;

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(5, ErrorMessage = "کلمه عبور باید بزرگتر ار 5 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RedirectTo { get; set; }
        public IActionResult OnGet(string redirectTo)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            RedirectTo = redirectTo;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _authService.Login(new LoginCommand()
            {
                Password = Password,
                PhoneNumber = PhoneNumber
            });
            if (result.IsSuccess == false)
            {
                ModelState.AddModelError(nameof(PhoneNumber), result.MetaData.Message);
                return Page();
            }

            var token = result.Data.Token;
            var refreshToken = result.Data.RefreshToken;
            HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(7)
            });
            HttpContext.Response.Cookies.Append("refresh-token", refreshToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(10)
            });

            if (string.IsNullOrWhiteSpace(RedirectTo) == false)
            {
                return LocalRedirect(RedirectTo);
            }
            return Redirect("/");
        }
    }
}
