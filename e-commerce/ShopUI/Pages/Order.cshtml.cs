using Application.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopUI.Pages
{
    public class OrderModel : PageModel
    {
    
        //[BindProperty]  only if submitting forms
        public GetOrder.Response Order { get; set; }
        public void OnGet(string reference, [FromServices] GetOrder getOrder)
        {
            Order = getOrder.Do(reference);
        }
    }
}
