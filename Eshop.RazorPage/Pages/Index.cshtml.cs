﻿using Eshop.RazorPage.Models.Auth;
using Eshop.RazorPage.Services.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IAuthService _authService;
        public IndexModel(ILogger<IndexModel> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task OnGet()
        {
       
        }
    }
}