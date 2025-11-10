using Microsoft.AspNetCore.Mvc;

namespace LoopSocialApp.Controllers
{
    public class FavoritesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
