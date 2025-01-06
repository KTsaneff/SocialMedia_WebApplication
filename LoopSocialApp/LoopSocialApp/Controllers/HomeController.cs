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

            //Check if image is uploaded
            if (post.Image != null && post.Image.Length > 0)
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (post.Image.ContentType.Contains("image"))
                {
                    string rootFolderPathImages = Path.Combine(rootFolderPath, "images/uploaded");
                    if (!Directory.Exists(rootFolderPathImages))
                    {
                        Directory.CreateDirectory(rootFolderPathImages);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(post.Image.FileName);
                    string filePath = Path.Combine(rootFolderPathImages, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await post.Image.CopyToAsync(fileStream);
                    }

                    newPost.ImageUrl = $"/images/uploaded/{fileName}";
                }
            }

            //Add post to database
            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();

            //Redirect to home page
            return RedirectToAction("Index");
        }
    }
}
