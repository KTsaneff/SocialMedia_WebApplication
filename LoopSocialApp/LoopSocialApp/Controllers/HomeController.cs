using LoopSocialApp.Data;
using LoopSocialApp.Data.DataModels;
using LoopSocialApp.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allPosts = await _context.Posts
                .Include(n => n.ApplicationUser)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();

            return View(allPosts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostViewModel post)
        {
            //Hardcode existing user
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            //Create new post
            var newPost = new Post
            {
                Content = post.Content,
                ImageUrl = string.Empty,
                ApplicationUserId = userId,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                NumberOfReposrts = 0
            };

            //Add post to database
            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();

            //Redirect to home page
            return RedirectToAction("Index");
        }
    }
}
