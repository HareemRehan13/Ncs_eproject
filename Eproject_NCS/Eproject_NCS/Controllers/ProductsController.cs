    using Eproject_NCS.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eproject_NCS.Controllers
{
	public class ProductsController : Controller
	{
		NexusContext db = new NexusContext();
		public IActionResult Index()
		{
            var pdata = db.Products.ToList();
            return View(pdata.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product pro, IFormFile file)
        {

            string imageName = DateTime.Now.ToString("yymmddhhmmss");//6432647443473
            imageName += Path.GetFileName(file.FileName);//6432647443473apple.jpg
            var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/uploads");
            var imageValue = Path.Combine(imagepath, imageName);

            using (var stream = new FileStream(imageValue, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var dbimage = Path.Combine("/uploads", imageName);

            pro.Image = dbimage;

            db.Products .Add(pro);
            db.SaveChanges();

           

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = db.Products.Find(id);
            if (item == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                
                return View(item);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product item, IFormFile file, string oldImage)
        {
            var dbimage = "";
            if (file != null && file.Length > 0)
            {
                string imageName = DateTime.Now.ToString("yymmddhhmmss");//6432647443473
                imageName += Path.GetFileName(file.FileName);//6432647443473apple.jpg
                var imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/uploads");
                var imageValue = Path.Combine(imagepath, imageName);
                using (var stream = new FileStream(imageValue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                dbimage = Path.Combine("/uploads", imageName);
                item.Image = dbimage;
                db.Products .Update(item);
                db.SaveChanges();
            }
            else
            {
                item.Image = oldImage;
                db.Products .Update(item);
                db.SaveChanges();
            }

         
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = db.Products.Find(id);

            if (item == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View(item);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product item)
        {
            db.Products.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var item = db.Products.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
    }
}
