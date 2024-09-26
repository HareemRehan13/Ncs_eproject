using Eproject_NCS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Diagnostics;

namespace Eproject_NCS.Controllers
{
	public class HomeController : Controller
	{
		private readonly NexusContext db;
		public HomeController(NexusContext _db)
		{
			db = _db;
		}


		[Authorize(Roles = "User")]
        public IActionResult Index()
        {
          return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult Fetchdetails()
        {
            
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var checkCust = db.Customers.FirstOrDefault(c => c.Email == userEmail);
            return View(checkCust);

        }
        [Authorize]
		public IActionResult Plans()
		{
            var sdata = db.ServicePlans.ToList();
            return View(sdata.ToList());
        }
        [Authorize]
        public IActionResult DiscountSchemes()
        {
            var dsdata = db.DiscountSchemes.ToList();
            return View(dsdata.ToList());
        }
		[Authorize]
		public IActionResult Products()
        {
            var pdata = db.Products.ToList();
            return View(pdata.ToList());
        }
		[Authorize]
		public IActionResult AddCustomerDetails()
        {
            return View();

        }
		[Authorize]
		[HttpPost]
        public IActionResult AddCustomerDetails(Customer cust)
        {
            var checkCust = db.Customers.FirstOrDefault(b => b.Email == cust.Email);

            if (checkCust != null)
            {
                HttpContext.Session.SetInt32("CustId", cust.CustomerId);
                return RedirectToAction("AddConnectionOrder");

            }
            else
            {
                var addedCustomer = db.Customers.Add(cust);
                db.SaveChanges();
                HttpContext.Session.SetInt32("CustId", addedCustomer.Entity.CustomerId);
                return RedirectToAction("AddConnectionOrder");

            }


        }
        public IActionResult AddConnectionOrder()
        {
            ViewBag.PlanId = new SelectList(db.ServicePlans, "PlanId", "PlanType");
            return View();

        }
		[Authorize]
		[HttpPost]
        public IActionResult AddConnectionOrder(ConnectionOrder conor)
        {
            ViewBag.PlanId = new SelectList(db.ServicePlans, "PlanId", "PlanType");

            Random random = new Random();
            long id = (long)(random.NextDouble() * 1_000_000_000_00); // Generates a random number up to 11 digits

            string conorderid = id.ToString("D11"); ;

            var plan = db.ServicePlans.Find(conor.PlanId);

            int customerId = Convert.ToInt32(HttpContext.Session.GetInt32("CustId"));
            conor.ConordId = conorderid;
            conor.CustomerId = customerId;
            conor.Total = (int?)(plan.Price + plan.SecurityDeposit);
           
                db.ConnectionOrders.Add(conor);
                db.SaveChanges();
                return View("Index");
            
               
        }


    }
}
