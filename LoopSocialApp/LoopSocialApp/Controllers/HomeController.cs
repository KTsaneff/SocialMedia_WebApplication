using LoopSocialApp.Data;
using LoopSocialApp.Data.DataModels;
using LoopSocialApp.Data.Services.Interfaces;
using LoopSocialApp.Data.Utilities;
using LoopSocialApp.Data.Utilities.Enums;
using LoopSocialApp.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly IHashtagService _hashtagService;
        private readonly IFilesService _filesService;

        public HomeController(ILogger<HomeController> logger, 
            IPostService postService, 
            IHashtagService hashtagService,
            IFilesService filesService)
        {
            _logger = logger;
            _postService = postService;
            _hashtagService = hashtagService;
            _filesService = filesService;
        }

        public async Task<IActionResult> Index()
        {
            var loggedInUserId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            var allPosts = await _postService.GetAllPostsAsync(loggedInUserId);

            return View(allPosts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostVM post)
        {
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            var imageFileType = ImageFileType.PostImage;
            var imageUploadPath = await _filesService.UploadImageAsync(post.Image, imageFileType);

            var newPost = new Post
            {
                Content = post.Content,
                ImageUrl = imageUploadPath,
                ApplicationUserId = userId,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                NumberOfReports = 0
            };

            await _postService.CreatePostAsync(newPost);
            await _hashtagService.ProcessHashtagsForNewPostAsync(newPost.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostLike(PostLikeVM postLikeModel)
        {
            var loggedInUser = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            await _postService.TogglePostLikeAsync(postLikeModel.PostId, loggedInUser);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostFavorite(PostFavoriteVM postFavoriteModel)
        {
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            await _postService.TogglePostFavoritesAsync(postFavoriteModel.PostId, userId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostVisibility(PostVisibilityVM postVisibilityModel)
        {
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            await _postService.TogglePostVisibilityAsync(postVisibilityModel.PostId, userId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostComment(PostCommentVM postCommentModel)
        {
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            var newComment = new Comment
            {
                Content = postCommentModel.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                ApplicationUserId = userId,
                PostId = postCommentModel.PostId
            };
            await _postService.AddPostCommentAsync(newComment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostReport(PostReportVM postReportModel)
        {
            var userId = "00c185e1-7e41-4a01-9643-28ed5c8233ba";

            await _postService.ReportPostAsync(postReportModel.PostId, userId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PostRemove(PostRemoveVM postRemoveModel)
        {
            await _postService.RemovePostAsync(postRemoveModel.PostId);
            await _hashtagService.ProcessHashtagsForRemovedPostAsync(postRemoveModel.PostId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemovePostComment(RemoveCommentVM removeCommentModel)
        {
            await _postService.RemovePostCommentAsync(removeCommentModel.CommentId);

            return RedirectToAction("Index");
        }
    }
}
