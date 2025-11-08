using LoopSocialApp.Data.Services.Interfaces;
using LoopSocialApp.Data.Utilities.Enums;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace LoopSocialApp.Data.Services
{
    public class FilesService : IFilesService
    {
        public async Task<string> UploadImageAsync(IFormFile file, ImageFileType imageFileType)
        {
            string subFolder = imageFileType switch
            {
                ImageFileType.ProfilePicture => Path.Combine("images", "profile"),
                ImageFileType.StoryImage => Path.Combine("images", "stories"),
                ImageFileType.PostImage => Path.Combine("images", "posts"),
                ImageFileType.CoverImage => Path.Combine("images", "cover"),
                _ => throw new ArgumentException("Invalid file type!")
            };

            if (file != null && file.Length > 0)
            {
                string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (file.ContentType.Contains("image/", StringComparison.OrdinalIgnoreCase))
                {
                    string targetDir = Path.Combine(root, subFolder);
                    Directory.CreateDirectory(targetDir);

                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string fullPath = Path.Combine(targetDir, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                        await file.CopyToAsync(stream);

                    var webPath = $"/{subFolder.Replace(Path.DirectorySeparatorChar, '/')}/{fileName}";
                    return webPath;
                }
            }
            return "";
        }
    }
}
