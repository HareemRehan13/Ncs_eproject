using Eproject_NCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Eproject_NCS.Controllers
{
    public class ConnectionOrderController : Controller
    {
       
            NexusContext db = new NexusContext();
       
            public IActionResult Index()
        {
            var itemdata = db.ConnectionOrders.Include(c => c.Plan).ToList();

            return View(itemdata);
        }
        public IActionResult Create()
        {
            ViewBag.PlanId = new SelectList(db.ServicePlans, "PlanId", "PlanType");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(ConnectionOrder conor1)
        {

            if(ModelState.IsValid)
            {
                db.ConnectionOrders.Add(conor1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlanId = new SelectList(db.ServicePlans, "PlanId", "PlanType");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            return View();
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = db.ConnectionOrders.Find(id);
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
        public IActionResult Edit(ConnectionOrder conor1)
        {
            if (ModelState.IsValid)
            {
                db.ConnectionOrders.Update(conor1);
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
            var ConnectionOrder = db.ConnectionOrders.FirstOrDefault(x => x.CustomerId == id);
            return View(ConnectionOrder);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ConnectionOrder conor1)
        {

            db.ConnectionOrders.Remove(conor1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActivateConnection(string ConordId)
        {
        

            var Conord = db.ConnectionOrders.FirstOrDefault(x=>x.ConordId== ConordId);
            Conord.Status = "Activated";
                db.ConnectionOrders.Update(Conord);
                db.SaveChanges();

            var customerDetails=db.Customers.Find(Conord.CustomerId);
            if(customerDetails != null) {


                Random random = new Random();
                long id = (long)(random.NextDouble() * 1_000_000_000_000_00); // Generates a random number up to 11 digits

                string accountId = id.ToString("D14");
                if(Conord.PlanId == 6 || Conord.PlanId == 10)
                {
                    accountId = "DU" + accountId;//DU3473746233447

                }else if (Conord.PlanId == 7 || Conord.PlanId == 9)
                {
                    accountId = "BB" + accountId;
                }
                else if (Conord.PlanId == 8 )
                {
                    accountId = "TP" + accountId;
                }
                customerDetails.AccountId = accountId;
                db.Customers.Update(customerDetails);
                db.SaveChanges();

                string ConnectionNo = id.ToString("D14");
                string type = "0";
                if (Conord.PlanId == 6 || Conord.PlanId == 10)
                {
                    type = "1";
                    ConnectionNo = "DU" + ConnectionNo;//DU3473746233447

                }
                else if (Conord.PlanId == 7 || Conord.PlanId == 9)
                {
                    type = "2";
                    ConnectionNo = "BB" + ConnectionNo;
                }
                else if (Conord.PlanId == 8)
                {

                    type = "3";
                    ConnectionNo = "TP" + ConnectionNo;
                }

                Connection con = new Connection()
                {
                    CustomerId = customerDetails.CustomerId,
                    ConnectionNo = ConnectionNo,
                    Status = "Active",
                    PlanId = Conord.PlanId,
                    StartDate = DateTime.Now,
                    ConnectionType = Convert.ToString(type),


            };

                db.Connections.Add(con);
                db.SaveChanges();


            }

                return RedirectToAction("Index");

        }
    }
}
