using Eproject_NCS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eproject_NCS.Controllers
{
    public class OrdersController : Controller
    {
        NexusContext db = new NexusContext();
        public IActionResult Index()
        {

            var itemdata = db.Orders.Include(o => o.Customer).Include(o => o.Equipment).ToList();

            return View(itemdata);
        }

        //[HttpGet]
        //public IActionResult Create(int id )
        //{

        //    ViewBag.EquipmentId = id;

        //    return View();
        //}


        [Authorize(Roles ="User")]
        [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(int  ProductId)
    {
            Order ord= new Order();
            ord.EquipmentId = ProductId;
            Random random = new Random();
            long id = (long)(random.NextDouble() * 1_000_000_000_00); // Generates a random number up to 11 digits
          
            string orderId = id.ToString("D11"); ;

            int cid = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            ord.CustomerId = cid;
            ord.OrderId = orderId;
            db.Orders.Add(ord);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        
    //    For Connection
    //if(connection.planID ==1){
            
    //        connectionno= "D"+connectionno;BD3648723684364237
    //        }
   

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var item = db.Orders.Find(id);
        if (item == null)
        {
            return RedirectToAction("index");
        }
        else
        {

                ViewBag.EquipmentId = new SelectList(db.Products, "ProductId", "ProductName");
        

                return View(item);
        }

    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Order ord1)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Update(ord1);
                db.SaveChanges();
                ViewBag.EquipmentId = new SelectList(db.Products, "ProductId", "ProductName");
               

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
            var Order = db.Orders.Find(id);
            return View(Order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Order ord1)
        {

            db.Orders.Remove(ord1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
		public IActionResult Details(int id)
		{
			var order = db.Orders
				.Include(o => o.Customer)
				.Include(o => o.Equipment)
				.FirstOrDefault(o => o.OrderId == id.ToString());

			if (order == null)
			{
				return NotFound();
			}
			return View(order);
		}



	}
}

