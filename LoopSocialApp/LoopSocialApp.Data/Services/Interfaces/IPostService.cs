using LoopSocialApp.Data.DataModels;
using Microsoft.AspNetCore.Http;

namespace LoopSocialApp.Data.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync(string loggedInUserId);
        Task<Post> CreatePostAsync(Post post, IFormFile image);
        Task RemovePostAsync(int postId);
        Task AddPostCommentAsync(Comment coment);
        Task RemovePostCommentAsync(int commentId);
        Task TogglePostLikeAsync(int postId, string userId);
        Task TogglePostFavoritesAsync(int postId, string userId);
        Task ReportPostAsync(int postId, string userId);
    }
}