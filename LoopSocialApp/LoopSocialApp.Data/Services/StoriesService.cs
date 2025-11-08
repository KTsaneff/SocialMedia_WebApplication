using LoopSocialApp.Data.DataModels;
using LoopSocialApp.Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Data.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly AppDbContext _context;

        public StoriesService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Story>> GetAllStoriesAsync()
        {
            return await _context.Stories
                .Where(s => s.DateCreated >= DateTime.UtcNow.AddHours(-24))
                .Include(s => s.ApplicationUser)
                .ToListAsync();
        }

        public async Task<Story> CreateStoryAsync(Story newStory, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (image.ContentType.Contains("image"))
                {
                    string rootFolderImages = Path.Combine(rootFolderPath, "images/stories");
                    Directory.CreateDirectory(rootFolderImages);

                    string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(rootFolderImages, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await image.CopyToAsync(stream);

                    newStory.ImageUrl = "/images/stories/" + fileName;
                }
            }

            await _context.Stories.AddAsync(newStory);
            await _context.SaveChangesAsync();

            return newStory;
        }
    }
}
