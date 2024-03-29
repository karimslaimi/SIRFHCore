﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SIRHCoreWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class MfaModel : PageModel
    {
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }
}
