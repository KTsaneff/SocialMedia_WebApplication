using LoopSocialApp.Data;
using LoopSocialApp.Data.DataModels;
using LoopSocialApp.ViewModels.Stories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Controllers
{
    public class StoriesController : Controller
    {
        private readonly AppDbContext _context;

        public StoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStory(StoryVM storyVM)
        {
            var loggedInUserId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            var newStory = new Story
            {
                DateCreated = DateTime.UtcNow,
                IsDeleted = false,
                ApplicationUserId = loggedInUserId
            };

            if(storyVM.Image != null && storyVM.Image.Length > 0)
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (storyVM.Image.ContentType.Contains("image"))
                {
                    string rootFolderImages = Path.Combine(rootFolderPath, "images/stories");
                    Directory.CreateDirectory(rootFolderImages);

                    string fileName = Guid.NewGuid() + Path.GetExtension(storyVM.Image.FileName);
                    string filePath = Path.Combine(rootFolderImages, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await storyVM.Image.CopyToAsync(stream);

                    newStory.ImageUrl = "/images/stories/" + fileName;
                }
            }

            await _context.Stories.AddAsync(newStory);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
