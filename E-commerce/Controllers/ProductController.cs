using E_commerce.Data;
using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
    public class ProductController : Controller
    {
      ApplicationDbContext dbcontext = new ApplicationDbContext();
        public IActionResult Index()
        {
            var products = dbcontext.products.Include(e => e.Category).ToList();
            return View(products);
        }
        [HttpGet]

        public IActionResult Newproduct()
        {
            var products = dbcontext.Categories.ToList();
            return View(products);
        }
        [HttpPost]
        public IActionResult Newc(Product product ,IFormFile ImgUrl)
        
        {
            if (ImgUrl.Length > 0)
            {
                var FileName = ImgUrl.FileName;
                var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", FileName);

                using (var Stream = System.IO.File.Create(FilePath))
                {
                    ImgUrl.CopyTo(Stream);
                }
                product.ImgUrl = ImgUrl.FileName;
            }


           dbcontext.products.Add(product);
            dbcontext.SaveChanges();

            return RedirectToAction("Index");


        }
        public IActionResult Delete(int id)
        {
            Product product  = new Product() { Id = id };
            dbcontext.products.Remove(product);
            dbcontext.SaveChanges();


            return RedirectToAction("Index");


        }
    }
}
