using Application.Cart;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace ShopUI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public string PublicKey { get; }
        private ApplicationDbContext _context;
        public PaymentModel(IConfiguration config, ApplicationDbContext context)
        {
            PublicKey = config["stripe:PublicKey"].ToString();
            _context = context;
        }


        public IActionResult OnGet()
        {
   
            var customerInformation = new GetCustomerInformation(HttpContext.Session).Do();
            if (customerInformation == null)
                return RedirectToPage("/Checkout/CustomerInformation");

            return Page();
        }
        public IActionResult OnPost(string stripeEmail,string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var cartOrder = new GetOrder(HttpContext.Session, _context).Do();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = cartOrder.GetTotalCharge(),
                Description = "Shop Purchase",
                Currency = "gbp",
                Customer = customer.Id
            });

            return RedirectToPage("/Index");
        }

    }
}
