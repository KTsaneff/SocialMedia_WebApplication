using LoopSocialApp.Data.DataModels;
using LoopSocialApp.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Data.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppDbContext _context;

        public UsersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUser> GetUserByIdAsync(string loggedInUserId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == loggedInUserId)
                ?? new ApplicationUser();
        }
    }
}
