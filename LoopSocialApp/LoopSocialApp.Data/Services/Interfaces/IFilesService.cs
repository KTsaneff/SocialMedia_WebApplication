using LoopSocialApp.Data.Utilities.Enums;
using Microsoft.AspNetCore.Http;

namespace LoopSocialApp.Data.Services.Interfaces
{
    public interface IFilesService
    {
        Task<string> UploadImageAsync(IFormFile file, ImageFileType imageFileType);
    }
}
