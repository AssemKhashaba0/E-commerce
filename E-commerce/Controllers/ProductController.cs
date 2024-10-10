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
                var FileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", FileName);

                using (var Stream = System.IO.File.Create(FilePath))
                {
                    ImgUrl.CopyTo(Stream);
                }
                product.ImgUrl = FileName;
            }


           dbcontext.products.Add(product);
            dbcontext.SaveChanges();

            return RedirectToAction("Index");


        }

        public IActionResult Edit(int productId)
        {
            var product = dbcontext.products.Find(productId);
            var category = dbcontext.Categories.ToList();

            ViewBag.allcategory = category;
            if (product != null)
            {
                return View( product);
            }
            return RedirectToAction("NotFoundPage", "Home");

        }
        //الجزء ده كله مش فاهم فيه حاجه 

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile ImgUrl)
        {
            // جلب المنتج القديم بدون تتبع
            var oldProduct = dbcontext.products.AsNoTracking().FirstOrDefault(e => e.Id == product.Id);

            // التحقق من وجود صورة جديدة
            if (ImgUrl != null && ImgUrl.Length > 0)
            {
                // إذا كانت هناك صورة قديمة، قم بحذفها
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", oldProduct.ImgUrl);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath); // حذف الصورة القديمة
                }

                // حفظ الصورة الجديدة
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }

                // حفظ اسم الصورة الجديدة في المنتج
                product.ImgUrl = fileName;
            }
            else
            {
                // إذا لم يتم تحميل صورة جديدة، الاحتفاظ بالصورة القديمة
                product.ImgUrl = oldProduct.ImgUrl;
            }

            // تحديث المنتج في قاعدة البيانات
            dbcontext.products.Update(product);
            dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }
        //لحد هنا 

        public IActionResult Delete(int id)
        {
            Product product  = new Product() { Id = id };
            dbcontext.products.Remove(product);
            dbcontext.SaveChanges();


            return RedirectToAction("Index");


        }
    }
}
