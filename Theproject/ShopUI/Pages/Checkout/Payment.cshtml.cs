using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopUI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public IActionResult OnGet()
        {
            var customerInformation = new GetCustomerInformation(HttpContext.Session).Do();
            if (customerInformation == null)
                return RedirectToPage("/Checkout/CustomerInformation");

            return Page();
        }

    }
}
