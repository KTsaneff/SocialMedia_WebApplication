using Microsoft.AspNetCore.Mvc;

namespace LoopSocialApp.ViewComponents
{
    public class HashtagsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
