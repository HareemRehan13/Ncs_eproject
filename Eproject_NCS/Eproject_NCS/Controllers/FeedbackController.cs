using Microsoft.AspNetCore.Mvc;

namespace Eproject_NCS.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
