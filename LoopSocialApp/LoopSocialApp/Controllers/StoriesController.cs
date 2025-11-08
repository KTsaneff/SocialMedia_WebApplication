using LoopSocialApp.Data.DataModels;
using LoopSocialApp.Data.Services.Interfaces;
using LoopSocialApp.ViewModels.Stories;
using Microsoft.AspNetCore.Mvc;

namespace LoopSocialApp.Controllers
{
    public class StoriesController : Controller
    {
        private readonly IStoriesService _storiesService;
        private readonly IFilesService _filesService;

        public StoriesController(
            IStoriesService storiesService,
            IFilesService filesService)
        {
            _storiesService = storiesService;
            _filesService = filesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStory(StoryVM storyVM)
        {
            var loggedInUserId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";
            var imageFileType = Data.Utilities.Enums.ImageFileType.StoryImage;

            var imageUploadPath = await _filesService.UploadImageAsync(storyVM.Image!, imageFileType);

            var newStory = new Story
            {
                DateCreated = DateTime.UtcNow,
                IsDeleted = false,
                ImageUrl = imageUploadPath,
                ApplicationUserId = loggedInUserId
            };

            await _storiesService.CreateStoryAsync(newStory);

            return RedirectToAction("Index", "Home");
        }
    }
}
