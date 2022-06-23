using Application.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopUI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {


        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }
        public IActionResult OnGet()
        {
            var customerInformation = new GetCustomerInformation(HttpContext.Session).Do();
            if (customerInformation == null)
                return Page();
            else
                return RedirectToPage("/Checkout/Payment");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            new AddCustomerInformation(HttpContext.Session).Do(CustomerInformation);
            return RedirectToPage("/Checkout/Payment");
        }
    }
}
