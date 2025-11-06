using LoopSocialApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.ViewComponents
{
    public class HashtagsViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public HashtagsViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var top5recentHashtags = await _context.Hashtags
                .Where(h => h.DateCreated >= DateTime.UtcNow.AddDays(-7))
                .OrderByDescending(h => h.Count)
                .Take(5).ToListAsync();

            return View(top5recentHashtags);
        }
    }
}
