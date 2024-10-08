using E_commerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
    public class ProductController : Controller
    {
      ApplicationDbContext dbcontext = new ApplicationDbContext();
        public IActionResult Index()
        {
            var products = dbcontext.products.ToList();
            return View(products);
        }
    }
}
