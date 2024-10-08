using E_commerce.Data;
using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;

namespace E_commerce.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public IActionResult Index()
        {
            var categorys = dbContext.Categories.ToList();
            return View(model: categorys);
        }

        public IActionResult Newcategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Newcategory(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            
            return RedirectToAction("Index"); 


        }
        public IActionResult Edit(int categoryId)
        {
            var category = dbContext.Categories.Find(categoryId);
            if (category != null)
            {
                return View(category);
            }
            return RedirectToAction("NotFoundPage", "Home");

        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            dbContext.Categories.Update(category);
            dbContext.SaveChanges();
            
             return RedirectToAction(nameof(Index));

            


        }
        [HttpGet]

        public IActionResult Delete(int id)
        {
            Category category = new Category() { Id = id };
            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
           

            return RedirectToAction("Index");


        }

    }
}
