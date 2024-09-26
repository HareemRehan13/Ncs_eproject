using Eproject_NCS.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eproject_NCS.Controllers
{
	public class AdminController : Controller
	{
		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			
			return View();
		}
		public IActionResult AdminLogout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("adminemail");
			HttpContext.Session.Remove("role");
			return RedirectToAction("Login");

		}
		public IActionResult UserLogout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("useremail");
			HttpContext.Session.Remove("role");
			return RedirectToAction("Login");

		}
	}
}
