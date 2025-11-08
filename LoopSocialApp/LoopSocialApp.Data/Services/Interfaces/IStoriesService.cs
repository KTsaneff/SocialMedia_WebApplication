using LoopSocialApp.Data.DataModels;
using Microsoft.AspNetCore.Http;

namespace LoopSocialApp.Data.Services.Interfaces
{
    public interface IStoriesService
    {
        Task<List<Story>> GetAllStoriesAsync();
        Task<Story> CreateStoryAsync(Story story, IFormFile image);
    }
}
