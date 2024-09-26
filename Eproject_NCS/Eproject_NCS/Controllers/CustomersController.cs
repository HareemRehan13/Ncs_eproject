using Eproject_NCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eproject_NCS.Controllers
{
    public class CustomersController : Controller
    {

        NexusContext db = new NexusContext();
        public IActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Customer cus)
        {
            if (ModelState.IsValid)
            {

                db.Customers.Add(cus);
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
            var Customer = db.Customers.Find(id);
            return View(Customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer cus1)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Update(cus1);
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
            var Customer = db.Customers.Find(id);
            return View(Customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Customer cus1)
        {

            db.Customers.Remove(cus1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Details(int id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
    }
}
