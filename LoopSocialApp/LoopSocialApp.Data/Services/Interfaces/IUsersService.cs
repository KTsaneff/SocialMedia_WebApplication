using LoopSocialApp.Data.DataModels;

namespace LoopSocialApp.Data.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ApplicationUser> GetUserByIdAsync(string loggedInUserId);
    }
}
