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
            var loggedInUserId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            var allPosts = await _context.Posts
                .Where(n => !n.IsPrivate || n.ApplicationUserId == loggedInUserId)
                .Include(n => n.ApplicationUser)
                .Include(n => n.Likes)
                .Include(n => n.Favorites)
                .Include(n => n.Comments).ThenInclude(n => n.ApplicationUser)
                .Include(n => n.Reports)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();

            return View(allPosts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostVM post)
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

        [HttpPost]
        public async Task<IActionResult> TogglePostLike(PostLikeVM model)
        {
            //Hardcode existing user
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            //check if user has already liked the post
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(n => n.ApplicationUserId == userId && n.PostId == model.PostId);

            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
                await _context.SaveChangesAsync();
            }
            else
            {
                var newLike = new Like
                {
                    ApplicationUserId = userId,
                    PostId = model.PostId
                };
                await _context.Likes.AddAsync(newLike);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostFavorite(PostFavoriteVM model)
        {
            //Hardcode existing user
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            //check if user has already favorited the post
            var existingFavorite= await _context.Favorites
                .FirstOrDefaultAsync(n => n.PostId == model.PostId && n.ApplicationUserId == userId);

            if (existingFavorite != null)
            {
                _context.Favorites.Remove(existingFavorite);
                await _context.SaveChangesAsync();
            }
            else
            {
                var newFavorite = new Favorite
                {
                    ApplicationUserId = userId,
                    PostId = model.PostId,
                    DateCreated = DateTime.UtcNow
                };
                await _context.Favorites.AddAsync(newFavorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostVisibility(PostVisibilityVM model)
        {
            //Hardcode existing user
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            //get post from database
            var post = await _context.Posts
                .FirstOrDefaultAsync(n => n.Id == model.PostId && n.ApplicationUserId == userId);

            if (post != null)
            {
                post.IsPrivate = !post.IsPrivate;
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostComment(PostCommentVM model)
        {
            //Hardcode existing user
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            //Create a comment object
            var newComment = new Comment
            {
                Content = model.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                ApplicationUserId = userId,
                PostId = model.PostId
            };

            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostReport(PostReportVM model)
        {
            //Hardcode existing user
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            //Create a comment object
            var newReport = new Report
            {
                ApplicationUserId = userId,
                PostId = model.PostId,
                DateCreated = DateTime.UtcNow,
            };

            await _context.Reports.AddAsync(newReport);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemovePostComment(RemoveCommentVM model)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(n => n.Id == model.CommentId);

            if(comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
