using LoopSocialApp.Data.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } = null!;

        public DbSet<Story> Stories { get; set; } = null!;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Report> Reports { get; set; }

        public DbSet<Hashtag> Hashtags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(e =>
            {
                e.HasMany(u => u.Posts)
                 .WithOne(p => p.ApplicationUser)
                 .HasForeignKey(p => p.ApplicationUserId);

                e.HasMany(u => u.Stories)
                 .WithOne(s => s.ApplicationUser)
                 .HasForeignKey(s => s.ApplicationUserId);
            });

            builder.Entity<Like>(e =>
            {
                e.HasOne(l => l.Post)
                 .WithMany(p => p.Likes)
                 .HasForeignKey(l => l.PostId)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(l => l.Story)
                 .WithMany(s => s.Likes)
                 .HasForeignKey(l => l.StoryId)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(l => l.ApplicationUser)
                 .WithMany(u => u.Likes)
                 .HasForeignKey(l => l.ApplicationUserId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(l => new { l.ApplicationUserId, l.PostId })
                 .IsUnique()
                 .HasFilter("[PostId] IS NOT NULL");

                e.HasIndex(l => new { l.ApplicationUserId, l.StoryId })
                 .IsUnique()
                 .HasFilter("[StoryId] IS NOT NULL");

                e.ToTable(t => t.HasCheckConstraint(
                    "CK_Likes_PostOrStory",
                    "(CASE WHEN [PostId] IS NULL THEN 0 ELSE 1 END) + (CASE WHEN [StoryId] IS NULL THEN 0 ELSE 1 END) = 1"
                ));
            });

            builder.Entity<Comment>(e =>
            {
                e.HasOne(c => c.Post)
                 .WithMany(p => p.Comments)
                 .HasForeignKey(c => c.PostId)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(c => c.Story)
                 .WithMany(s => s.Comments)
                 .HasForeignKey(c => c.StoryId)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(c => c.ApplicationUser)
                 .WithMany(u => u.Comments)
                 .HasForeignKey(c => c.ApplicationUserId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.ToTable(t => t.HasCheckConstraint(
                    "CK_Comments_PostOrStory",
                    "(CASE WHEN [PostId] IS NULL THEN 0 ELSE 1 END) + (CASE WHEN [StoryId] IS NULL THEN 0 ELSE 1 END) = 1"
                ));
            });

            builder.Entity<Favorite>(e =>
            {
                e.HasOne(f => f.Post)
                 .WithMany(p => p.Favorites)
                 .HasForeignKey(f => f.PostId)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(f => f.Story)
                 .WithMany(s => s.Favorites)
                 .HasForeignKey(f => f.StoryId)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(f => f.ApplicationUser)
                 .WithMany(u => u.Favorites)
                 .HasForeignKey(f => f.ApplicationUserId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(f => new { f.ApplicationUserId, f.PostId })
                 .IsUnique()
                 .HasFilter("[PostId] IS NOT NULL");

                e.HasIndex(f => new { f.ApplicationUserId, f.StoryId })
                 .IsUnique()
                 .HasFilter("[StoryId] IS NOT NULL");

                e.ToTable(t => t.HasCheckConstraint(
                    "CK_Favorites_PostOrStory",
                    "(CASE WHEN [PostId] IS NULL THEN 0 ELSE 1 END) + (CASE WHEN [StoryId] IS NULL THEN 0 ELSE 1 END) = 1"
                ));
            });

            builder.Entity<Report>(e =>
            {
                e.HasOne(r => r.Post)
                 .WithMany(p => p.Reports)
                 .HasForeignKey(r => r.PostId)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(r => r.Story)
                 .WithMany(s => s.Reports)
                 .HasForeignKey(r => r.StoryId)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(r => r.ApplicationUser)
                 .WithMany(u => u.Reports)
                 .HasForeignKey(r => r.ApplicationUserId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(r => new { r.ApplicationUserId, r.PostId })
                 .IsUnique()
                 .HasFilter("[PostId] IS NOT NULL");

                e.HasIndex(r => new { r.ApplicationUserId, r.StoryId })
                 .IsUnique()
                 .HasFilter("[StoryId] IS NOT NULL");

                e.ToTable(t => t.HasCheckConstraint(
                    "CK_Reports_PostOrStory",
                    "(CASE WHEN [PostId] IS NULL THEN 0 ELSE 1 END) + (CASE WHEN [StoryId] IS NULL THEN 0 ELSE 1 END) = 1"
                ));
            });

            builder.Entity<Story>(e =>
            {
                e.HasMany(s => s.Comments)
                 .WithOne(c => c.Story)
                 .HasForeignKey(c => c.StoryId)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasMany(s => s.Reports)
                 .WithOne(r => r.Story)
                 .HasForeignKey(r => r.StoryId)
                 .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
