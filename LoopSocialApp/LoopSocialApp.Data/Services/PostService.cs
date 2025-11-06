using LoopSocialApp.Data.DataModels;
using LoopSocialApp.Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LoopSocialApp.Data.Services
{
    public class PostService : IPostService
    {
        private AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public Task AddPostCommentAsync(Comment coment)
        {
            throw new NotImplementedException();
        }

        public Task<Post> CreatePostAsync(Post post, IFormFile image)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetAllPostsAsync(string loggedInUserId)
        {
            throw new NotImplementedException();
        }

        public Task RemovePostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task RemovePostCommentAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task ReportPostAsync(int postId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task TogglePostFavoritesAsync(int postId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task TogglePostLikeAsync(int postId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
