using LoopSocialApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.ViewComponents
{
    public class StoriesViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public StoriesViewComponent(AppDbContext context)
        {   
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allStories = await _context.Stories
                .Where(s => s.DateCreated >= DateTime.UtcNow.AddHours(-24))
                .Include(s => s.ApplicationUser)
                .ToListAsync();

            return View(allStories);
        }
    }
}
