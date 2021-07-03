using Application.Products;
using Application.Products.ViewModels;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _ctx;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

      //  [BindProperty]
       // public ProductViewModel Product { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
 
        public void OnGet()
        {
            Products = new GetProducts(_ctx).Do();
        }
        //public async  Task<IActionResult> OnPost()  
        //{
        //    await new CreateProducts(_ctx).Do(Product);
        //    return RedirectToPage("Index");
        //}
    }
}
