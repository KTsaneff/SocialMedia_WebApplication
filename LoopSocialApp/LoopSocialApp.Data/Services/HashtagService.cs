using LoopSocialApp.Data.DataModels;
using LoopSocialApp.Data.Services.Interfaces;
using LoopSocialApp.Data.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Data.Services
{
    public class HashtagService : IHashtagService
    {
        private readonly AppDbContext _context;

        public HashtagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task ProcessHashtagsForNewPostAsync(int postId)
        {
            List<string> postHashtags = await ScanPostForHashtagsAsync(postId);
            foreach (var hashtag in postHashtags)
            {
                var hashtagDb = await _context.Hashtags.FirstOrDefaultAsync(h => h.Name == hashtag);
                if (hashtagDb != null)
                {
                    hashtagDb.Count++;
                    hashtagDb.DateUpdated = DateTime.UtcNow;

                    _context.Hashtags.Update(hashtagDb);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Hashtag hashtagToAdd = new Hashtag()
                    {
                        Name = hashtag,
                        Count = 1,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    };
                    await _context.Hashtags.AddAsync(hashtagToAdd);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task ProcessHashtagsForRemovedPostAsync(int postId)
        {
            
            List<string> postHashtags = await ScanPostForHashtagsAsync(postId);
            foreach (var hashtag in postHashtags)
            {
                var hashtagDb = await _context.Hashtags.FirstOrDefaultAsync(h => h.Name == hashtag);
                if (hashtagDb != null && hashtagDb.Count > 0)
                {
                    hashtagDb.Count -= 1;
                    hashtagDb.DateUpdated = DateTime.UtcNow;

                    _context.Hashtags.Update(hashtagDb);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<string>> ScanPostForHashtagsAsync(int postId)
        {
            Post? postToScan = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            string content = "";
            if (postToScan != null)
            {
                content = postToScan.Content;
            }

            return HashtagHelper.GetHashtags(content);
        }
    }
}
