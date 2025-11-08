using LoopSocialApp.Data.DataModels;

namespace LoopSocialApp.Data.Services.Interfaces
{
    public interface IStoriesService
    {
        Task<List<Story>> GetAllStoriesAsync();
        Task<Story> CreateStoryAsync(Story story);
    }
}
