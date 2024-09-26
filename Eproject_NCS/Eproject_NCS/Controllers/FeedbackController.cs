using Eproject_NCS.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eproject_NCS.Controllers
{
    public class FeedbackController : Controller
    {

        NexusContext db = new NexusContext();

        public IActionResult Index()
        {


            var itemdata = db.Feedbacks.Include(c => c.Customer).
                Include(c => c.Order).ToList();

            return View(itemdata);
        }
        [HttpGet]

        public IActionResult Create()
        {

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId");
            return View();
        }

    }
}
