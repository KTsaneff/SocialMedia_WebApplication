using System.Text.RegularExpressions;

namespace LoopSocialApp.Data.Utilities
{
    public static class HashtagHelper
    {
        public static List<string> GetHashtags(string postText)
        {
            var hashtagPattern = new Regex(@"#\w+");
            var matches = hashtagPattern.Matches(postText)
                .Select(match => match.Value.TrimEnd('.',',','!','?'))
                .Distinct()
                .ToList();

            return matches;
        }
    }
}
