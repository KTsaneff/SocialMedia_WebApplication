using LoopSocialApp.Data.DataModels;

namespace LoopSocialApp.Data.Services.Interfaces
{
    public interface IHashtagService
    {
        Task ProcessHashtagsForNewPostAsync(int postyId);
        Task ProcessHashtagsForRemovedPostAsync(int postId);
        Task<List<string>> ScanPostForHashtagsAsync(int postId);
    }
}
