using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopUI.Pages.Account
{
    public class LoginModel : PageModel
    {
            private SignInManager<IdentityUser> _signInManager;

            public LoginModel(SignInManager<IdentityUser> signInManager)
            {
                _signInManager = signInManager;
            }

            [BindProperty]
            public LogInViewModel Input { get; set; }

            public void OnGet()
            {
            }

            public async Task<IActionResult> OnPost()
            {
            var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, false);

                if (result.Succeeded)
                    return RedirectToPage("/Admin/Index");

                return Page();
             }

    }

    public class LogInViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
      
    }
}
