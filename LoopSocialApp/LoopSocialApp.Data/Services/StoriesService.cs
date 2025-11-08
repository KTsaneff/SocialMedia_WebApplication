using LoopSocialApp.Data.DataModels;
using LoopSocialApp.Data.Services.Interfaces;
using LoopSocialApp.Data.Utilities.Enums;
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

        public async Task<Story> CreateStoryAsync(Story newStory)
        {
            await _context.Stories.AddAsync(newStory);
            await _context.SaveChangesAsync();

            return newStory;
        }
    }
}
