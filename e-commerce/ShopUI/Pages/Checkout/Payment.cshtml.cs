using Application.Cart;
using Application.Orders;
using Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Stripe;
using System.Linq;
using System.Threading.Tasks;
using GetOrderCart = Application.Cart.GetOrder;

namespace ShopUI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public string PublicKey { get; }
        public PaymentModel(IConfiguration config)
        {
            PublicKey = config["stripe:PublicKey"].ToString();
        }

        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation)
        {
            var customerInformation = getCustomerInformation.Do();
            if (customerInformation == null)
                return RedirectToPage("/Checkout/CustomerInformation");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            string stripeEmail,
            string stripeToken,
            [FromServices] GetOrderCart getOrder,
            [FromServices] CreateOrder createOrder,
            [FromServices] ISessionManager sessionManager)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var cartOrder = getOrder.Do();

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

            var sessionId = HttpContext.Session.Id;

            //create order
            await createOrder.Do(new CreateOrder.Request
            {
                StripeReference = charge.Id,
                SessionId = sessionId,
                FirstName = cartOrder.CustomerInformation.FirstName,
                LastName = cartOrder.CustomerInformation.LastName,
                Email = cartOrder.CustomerInformation.Email,
                PhoneNumber = cartOrder.CustomerInformation.PhoneNumber,
                Address1 = cartOrder.CustomerInformation.Address1,
                Address2 = cartOrder.CustomerInformation.Address2,
                City = cartOrder.CustomerInformation.City,
                PostCode = cartOrder.CustomerInformation.PostCode,
                Stocks = cartOrder.Products.Select(x => new CreateOrder.Stock
                {
                StockId = x.StockId,
                Num = x.Num,
                }).ToList(),
            });

            sessionManager.ClearCart();
            
            return RedirectToPage("/Index");

        }

    }
}
