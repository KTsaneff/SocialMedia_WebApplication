using LoopSocialApp.Data;
using LoopSocialApp.Data.DataModels;
using LoopSocialApp.Data.Services.Interfaces;
using LoopSocialApp.ViewModels.Stories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Controllers
{
    public class StoriesController : Controller
    {
        private readonly IStoriesService _storiesService;

        public StoriesController(IStoriesService storiesService)
        {
            _storiesService = storiesService;
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

            await _storiesService.CreateStoryAsync(newStory, storyVM.Image!);

            return RedirectToAction("Index", "Home");
        }
    }
}
