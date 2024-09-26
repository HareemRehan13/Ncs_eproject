using Eproject_NCS.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eproject_NCS.Controllers
{
	public class AuthController : Controller
	{
		private readonly NexusContext db;

		public AuthController(NexusContext _db)
		{
			db = _db;
		}

		public IActionResult Signup()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Signup(User user)
		{
			var checkUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
			if (checkUser == null)
			{
				var hasher = new PasswordHasher<string>();
				var hashPassword = hasher.HashPassword(user.Email, user.Password);
				user.Password = hashPassword;
				db.Users.Add(user);
				db.SaveChanges();
				return RedirectToAction("Login");
			}
			else
			{
				ViewBag.msg = "User already registered. Please login.";
				return View();
			}

		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Login(User user)
		{
			bool IsAuthenticated = false;

			ClaimsIdentity identity = null;
			string controller = "";
			var checkUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
			if (checkUser != null)
			{
				var hasher = new PasswordHasher<string>();
				var verifyPass = hasher.VerifyHashedPassword(user.Email, checkUser.Password, user.Password);
				if (verifyPass == PasswordVerificationResult.Success && checkUser.RoleId == 1)
				{
					identity = new ClaimsIdentity(new[]
				 {
					new Claim(ClaimTypes.Name ,checkUser.Username),
					new Claim(ClaimTypes.Role ,"Admin"),

				}
				, CookieAuthenticationDefaults.AuthenticationScheme);
					IsAuthenticated = true;
					controller = "Admin";

					HttpContext.Session.SetInt32("UserID", checkUser.Id);
					HttpContext.Session.SetString("UserEmail", checkUser.Email);

				}
				else if (verifyPass == PasswordVerificationResult.Success && checkUser.RoleId == 2)
				{
					identity = new ClaimsIdentity(new[]
			  {
					new Claim(ClaimTypes.Name ,checkUser.Username),
					new Claim(ClaimTypes.Role ,"User"),

				}
			 , CookieAuthenticationDefaults.AuthenticationScheme);
					IsAuthenticated = true;
					controller = "Home";
					HttpContext.Session.SetInt32("UserID", checkUser.Id);
					HttpContext.Session.SetString("UserEmail", checkUser.Email);

				}
				else
				{
					ViewBag.msg = "Invalid Credentials";
					return View();
				}


				if (IsAuthenticated)
				{
					var principal = new ClaimsPrincipal(identity);

					var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

					return RedirectToAction("Index", controller);
				}
				else
				{
					ViewBag.msg = "Lgoin Failed";
					return View();

				}
			}

			else
			{
				ViewBag.msg = "Invalid User";
				return View();
			}



		}


		public IActionResult Logout()
		{
			HttpContext.Session.Remove("UserID");
			HttpContext.Session.Remove("UserEmail");

			var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Auth");
		}

	}
}