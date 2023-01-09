using Application.Orders;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopUI.Pages
{
    public class OrderModel : PageModel
    {
        private ApplicationDbContext _context;

        public OrderModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //[BindProperty]  only if submitting forms
        public GetOrder.Response Order { get; set; }
        public void OnGet(string reference, [FromServices] GetOrder getOrder)
        {
            Order = getOrder.Do(reference);
        }
    }
}
