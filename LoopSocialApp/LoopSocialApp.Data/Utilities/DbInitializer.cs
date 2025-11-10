using LoopSocialApp.Data.DataModels;

namespace LoopSocialApp.Data.Utilities
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.ApplicationUsers.Any() && !context.Posts.Any())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Id = "d11679d1-9a2d-4b1d-a684-09e4aeb61b2c",
                        UserName = "nona_tsaneff",
                        FullName = "Nona Tsanev",
                        Email = "nonapetkova22@gmail.com",
                        ProfileImageUrl = "/images/profile/nona-tsaneva-profile.jpg"
                    },
                    new ApplicationUser
                    {
                        Id = "44543460-578c-48cd-b818-795f30b2e051",
                        UserName = "mladenov_nik",
                        FullName = "Nikola Mladenov",
                        Email = "nikola_d_m@gmail.com",
                        ProfileImageUrl = "/images/profile/nikola-mladenov-profile.jpg"
                    },
                    new ApplicationUser
                    {
                        Id = "c6ff8035-76d7-42b7-a4c6-5386d1198b29",
                        UserName = "Lina_Petkova",
                        FullName = "Tsvetelina Petkova",
                        Email = "where_tsveti_fits@gmail.com",
                        ProfileImageUrl = "/images/profile/tsvetelina-petkova-profile-img.jpg"
                    },
                    new ApplicationUser
                    {
                        Id = "00c185e1-7e41-4a01-9643-28ed5c8233ba",
                        UserName = "kss_tff",
                        FullName = "Krasimir Tsanev",
                        Email = "krassytsaneff@gmail.com",
                        ProfileImageUrl = "/images/profile/krassimir-tsanev-profile.jpg"
                    }

                };

                await context.ApplicationUsers.AddRangeAsync(users);
                await context.SaveChangesAsync();

                var posts = new List<Post>
                {
                    new Post
                    {
                        Content = "Отиваме към Малко Търново. Няма да чакам никой!",
                        ApplicationUserId = "44543460-578c-48cd-b818-795f30b2e051",
                        NumberOfReports = 2,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow,
                    },
                    new Post
                    {
                        Content = "Next week in Viena! Who wants to join us?",
                        ApplicationUserId = "d11679d1-9a2d-4b1d-a684-09e4aeb61b2c",
                        ImageUrl = "https://blog.karat-s.com/wp-content/uploads/2016/10/Wienr.jpg",
                        NumberOfReports = 0,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow,
                    },
                };

                await context.Posts.AddRangeAsync(posts);
                await context.SaveChangesAsync();
            }
        }
    }
}
