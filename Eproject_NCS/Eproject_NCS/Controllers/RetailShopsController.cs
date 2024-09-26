using Eproject_NCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eproject_NCS.Controllers
{
	public class RetailShopsController : Controller
	{
		NexusContext db = new NexusContext();
		public IActionResult Index()
		{
			return View(db.RetailShops.ToList());
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Create(RetailShop ret)
		{
			if (ModelState.IsValid)
			{

				db.RetailShops.Add(ret);
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
			var RetailShop = db.RetailShops.Find(id);
			return View(RetailShop);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(RetailShop ret1)
		{
			if (ModelState.IsValid)
			{
				db.RetailShops.Update(ret1);
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
			var RetailShop = db.RetailShops.Find(id);
			return View(RetailShop);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(RetailShop ret1)
		{

			db.RetailShops.Remove(ret1);
			db.SaveChanges();
			return RedirectToAction("Index");

		}
        public IActionResult Details(int id)
        {
            var retailShop = db.RetailShops.Find(id);
            if (retailShop == null)
            {
                return NotFound();
            }
            return View(retailShop);
        }
    }
}
