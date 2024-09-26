using Eproject_NCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eproject_NCS.Controllers
{
	public class ConnectionController : Controller
	{
		NexusContext db = new NexusContext();
		public IActionResult Index()
		{
            var condata = db.Connections.Include(c => c.Customer).Include(c => c.Plan).ToList();

            return View(condata);
        }

		[HttpGet]
		public IActionResult Create()
		{
			
			ViewBag.PlanId = new SelectList(db.ServicePlans, "PlanId", "PlanType");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult Create(Connection con)
        {

            Random random = new Random();
            long cno = (long)(random.NextDouble() * 1_000_000_000_00_00); 

            string connId = cno.ToString("D14"); ;


            //con.ConnectionId = conId;
            db.Connections.Add(con);
            db.SaveChanges();

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            ViewBag.PlanId = new SelectList(db.ServicePlans, "PlanId", "PlanType");
            return RedirectToAction("index");

            //if (Connection.PlanId == 6)
            //{

            //    cno = "D" + cno; //BD3648723684364237
            //}
        }

    }
}
