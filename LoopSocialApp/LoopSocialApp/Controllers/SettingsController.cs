using Microsoft.AspNetCore.Mvc;

namespace LoopSocialApp.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
