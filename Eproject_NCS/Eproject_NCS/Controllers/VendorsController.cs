using Eproject_NCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eproject_NCS.Controllers
{
		public class VendorsController : Controller
	{
		NexusContext db = new NexusContext();
		public IActionResult Index()
		{
			return View(db.Vendors.ToList());
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Create(Vendor ven)
		{
			if (ModelState.IsValid)
			{

				db.Vendors.Add(ven);
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
			var Vendor = db.Vendors.Find(id);
			return View(Vendor);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Vendor ven1)
		{
			if (ModelState.IsValid)
			{
				db.Vendors.Update(ven1);
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
			var Vendor = db.Vendors.Find(id);
			return View(Vendor);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Vendor ven1)
		{

			db.Vendors.Remove(ven1);
			db.SaveChanges();
			return RedirectToAction("Index");

		}
        public IActionResult Details(int id)
        {
            var vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }
    }
}
