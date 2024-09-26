using Eproject_NCS.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eproject_NCS.Controllers
{ 
	public class BillingController : Controller
    {

        NexusContext db = new NexusContext();
	
		public IActionResult Index()
        {
          

            var itemdata = db.Billings.Include(c => c.Connection).
                Include(c => c.Customer).ToList();

            return View(itemdata);
        }
        [HttpGet]
     
        public IActionResult Create()
        {

          
            ViewBag.ConnectionId = new SelectList(db.Connections, "ConnectionId", "ConnectionId");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Billing  conor1)
        {
          var cid=  conor1.ConnectionId;
            var details = db.Connections.FirstOrDefault(c => c.CustomerId==cid);

            var planid = details.PlanId;
            var plandetails = db.ServicePlans.FirstOrDefault(c => c.PlanId == planid);
            decimal total = (decimal)(plandetails.Price + plandetails.SecurityDeposit);

            conor1.TotalAmount = total;
            

            db.Billings.Add(conor1);
                db.SaveChanges();
            
            ViewBag.ConnectionId = new SelectList(db.Connections, "ConnectionId", "ConnectionId");
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = db.Billings.Find(id);
            if (item == null)
            {
                return RedirectToAction("index");
            }
            else
            {

                ViewBag.PlanId = new SelectList(db.ServicePlans, "PlanId", "PlanType");
                ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");

                return View(item);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Billing  conor1)
        {
            if (ModelState.IsValid)
            {
                db.Billings.Update(conor1);
                db.SaveChanges();
                ViewBag.PlanId = new SelectList(db.ServicePlans, "PlanId", "PlanType");
                ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
                ViewBag.CustomerId = new SelectList(db.ConnectionOrders, "CustomerId");
                return RedirectToAction("Index");

            }
            else
            {

                return View();
            }


        }
        [HttpGet]

        public IActionResult Delete(int id)
        {
            var Billing = db.Billings.Find(id);
            return View(Billing);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Billing conor1)
        {

            db.Billings.Remove(conor1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
		public IActionResult Details(int id)
		{
			var billing = db.Billings
				.Include(b => b.Connection) 
				.Include(b => b.Customer) 
				.FirstOrDefault(b => b.BillingId == id);

			if (billing == null)
			{
				return NotFound(); 
			}

			return View(billing); 
		}

	}
}

