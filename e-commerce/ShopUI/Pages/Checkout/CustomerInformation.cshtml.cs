using Application.Cart;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace ShopUI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private IWebHostEnvironment _env;

        public CustomerInformationModel(IWebHostEnvironment env)
        {
            _env = env;
                
        }
        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }
        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation)
        {
            var customerInformation = getCustomerInformation.Do();
            if (customerInformation == null)
            {
                if (_env.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInformation.Request
                    {
                        FirstName = "A",
                        LastName = "A",
                        Email = "A@g.com",
                        PhoneNumber = "02",
                        Address1 = "A",
                        Address2 = "A",
                        City = "A",
                        PostCode = "A",
                    };
                }
                return Page();

            }
            else
                return RedirectToPage("/Checkout/Payment");
        }

        public IActionResult OnPost([FromServices] AddCustomerInformation addCustomerInformation)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            addCustomerInformation.Do(CustomerInformation);
            return RedirectToPage("/Checkout/Payment");
        }
    }
}
