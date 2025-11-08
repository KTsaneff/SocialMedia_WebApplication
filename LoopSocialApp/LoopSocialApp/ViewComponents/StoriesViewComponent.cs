using LoopSocialApp.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoopSocialApp.ViewComponents
{
    public class StoriesViewComponent : ViewComponent
    {
        private readonly IStoriesService _storiesService;

        public StoriesViewComponent(IStoriesService storiesService)
        {
            _storiesService = storiesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _storiesService.GetAllStoriesAsync());
        }
    }
}
