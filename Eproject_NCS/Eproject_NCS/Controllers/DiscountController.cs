using Eproject_NCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eproject_NCS.Controllers
{
	public class DiscountController : Controller
	{
		NexusContext db = new NexusContext();
		public IActionResult Index()
		{
			return View(db.DiscountSchemes.ToList());
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Create(DiscountScheme dis)
		{
			if (ModelState.IsValid)
			{

				db.DiscountSchemes.Add(dis);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{

				return View();
			}
		}
		public IActionResult Edit(int id)
		{
			var DiscountScheme = db.DiscountSchemes.Find(id);
			return View(DiscountScheme);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(DiscountScheme dis1)
		{
			if (ModelState.IsValid)
			{
				db.DiscountSchemes.Update(dis1);
				db.SaveChanges();
				return RedirectToAction("Index");

			}
			else
			{

				return View();
			}


		}
		public IActionResult Delete(int id)
		{
			var DiscountScheme = db.DiscountSchemes.Find(id);
			return View(DiscountScheme);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(DiscountScheme dis1)
		{

			db.DiscountSchemes.Remove(dis1);
			db.SaveChanges();
			return RedirectToAction("Index");

		}
        public IActionResult Details(int id)
        {
            var discountScheme = db.DiscountSchemes.Find(id);
            if (discountScheme == null)
            {
                return NotFound();
            }
            return View(discountScheme);
        }
    }
}
