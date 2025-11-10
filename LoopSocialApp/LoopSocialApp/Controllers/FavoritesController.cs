using LoopSocialApp.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoopSocialApp.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IPostService _postService;
        private string userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

        public FavoritesController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var myFavoritePosts = await _postService.GetAllFavoritePostsAsync(this.userId);

            return View(myFavoritePosts);
        }
    }
}
