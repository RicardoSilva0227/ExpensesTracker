using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
