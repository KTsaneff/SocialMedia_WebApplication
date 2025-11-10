using LoopSocialApp.Data.DataModels;
using LoopSocialApp.Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Data.Services
{
    public class PostService : IPostService
    {
        private AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsAsync(string loggedInUserId)
        {
            return await _context.Posts
                .Where(n => (!n.IsPrivate || n.ApplicationUserId == loggedInUserId)
                             && n.Reports.Count < 5 && !n.IsDeleted)
                .Include(n => n.ApplicationUser)
                .Include(n => n.Likes)
                .Include(n => n.Favorites)
                .Include(n => n.Comments).ThenInclude(n => n.ApplicationUser)
                .Include(n => n.Reports)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();
        }
        public async Task<Post> CreatePostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }
        public async Task RemovePostAsync(int postId)
        {
            var postDb = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if(postDb != null)
            {
                //_context.Posts.Remove(postDb);
                postDb.IsDeleted = true;
                _context.Posts.Update(postDb);
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemovePostCommentAsync(int commentId)
        {
            var commentDb = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if(commentDb != null)
            {
                _context.Comments.Remove(commentDb);
                await _context.SaveChangesAsync();

            }
        }
        public async Task ReportPostAsync(int postId, string userId)
        {
            var newReport = new Report()
            {
                PostId = postId,
                ApplicationUserId = userId,
                DateCreated = DateTime.UtcNow
            };

            await _context.Reports.AddAsync(newReport);
            await _context.SaveChangesAsync();
        }
        public async Task TogglePostFavoritesAsync(int postId, string userId)
        {
            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(n => n.PostId == postId && n.ApplicationUserId == userId);

            if (existingFavorite != null)
            {
                _context.Favorites.Remove(existingFavorite);
                await _context.SaveChangesAsync();
            }
            else
            {
                var newFavorite = new Favorite
                {
                    ApplicationUserId = userId,
                    PostId = postId,
                    DateCreated = DateTime.UtcNow
                };
                await _context.Favorites.AddAsync(newFavorite);
                await _context.SaveChangesAsync();
            }
        }
        public async Task TogglePostLikeAsync(int postId, string userId)
        {
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(n => n.ApplicationUserId == userId && n.PostId == postId);

            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
                await _context.SaveChangesAsync();
            }
            else
            {
                var newLike = new Like
                {
                    ApplicationUserId = userId,
                    PostId = postId
                };
                await _context.Likes.AddAsync(newLike);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Post>> GetAllFavoritePostsAsync(string userId)
        {
            var posts = await _context.Posts
                .Where(p => !p.IsDeleted
                         && p.Reports.Count < 5
                         && p.Favorites.Any(f => f.ApplicationUserId == userId))
                .Include(p => p.ApplicationUser)
                .Include(p => p.Comments).ThenInclude(c => c.ApplicationUser)
                .Include(p => p.Likes)
                .ToListAsync();

            return posts;
        }
        public async Task AddPostCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
        public async Task TogglePostVisibilityAsync(int postId, string userId)
        {
            var post = await _context.Posts
                .FirstOrDefaultAsync(n => n.Id == postId && n.ApplicationUserId == userId);

            if (post != null)
            {
                post.IsPrivate = !post.IsPrivate;
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
