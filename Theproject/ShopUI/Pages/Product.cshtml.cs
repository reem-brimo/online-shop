using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Products;
using Microsoft.AspNetCore.Http;

namespace ShopUI.Pages
{
    public class ProductModel : PageModel
    {
        private ApplicationDbContext _context;

        public ProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Test ProductTest { get; set; }
        public class Test
        {
            public string Id { get; set; }
        }

        public GetProduct.ProductViewModel Product { get; set; }
        public IActionResult OnGet(string name)
        {
            Product = new GetProduct(_context).Do(name.Replace("-", " "));
            if (Product == null)
                return RedirectToPage("index");
            else
                return Page();
        }

        public IActionResult OnPost()
        {
            var cuurent_id = HttpContext.Session.GetString("id");
            HttpContext.Session.SetString("id", ProductTest.Id);
            return RedirectToPage("Index");
        }
    }
}
