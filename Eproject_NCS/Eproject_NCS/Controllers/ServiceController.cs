using Eproject_NCS.Models;

using Microsoft.AspNetCore.Mvc;

namespace Eproject_NCS.Controllers
{
    public class ServiceController : Controller
    {
        NexusContext db = new NexusContext();
        public IActionResult Index()
        {
            return View(db.ServicePlans.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServicePlan ser)
        {
            if (ModelState.IsValid)
            {

                db.ServicePlans.Add(ser);
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
            var ServicePlan = db.ServicePlans.Find(id);
            return View(ServicePlan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServicePlan ser1)
        {
            if (ModelState.IsValid)
            {
                db.ServicePlans.Update(ser1);
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
            var Serviceplan = db.ServicePlans.Find(id);
            return View(Serviceplan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ServicePlan ser1)
        {

            db.ServicePlans.Remove(ser1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Details(int id)
        {
            var servicePlan = db.ServicePlans.Find(id);
            if (servicePlan == null)
            {
                return NotFound();
            }
            return View(servicePlan);
        }
    }
}
