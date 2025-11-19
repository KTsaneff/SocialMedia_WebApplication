using LoopSocialApp.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoopSocialApp.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IUsersService _usersService;

        public SettingsController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public async Task<IActionResult> Index()
        {
            var loggedInUserId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";
            var userDb = await _usersService.GetUserByIdAsync(loggedInUserId);
            return View(userDb);
        }
    }
}
