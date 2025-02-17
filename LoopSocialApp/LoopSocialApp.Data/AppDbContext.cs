using LoopSocialApp.Data.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {            
        }

        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.ApplicationUserId);

            builder.Entity<Like>()
                .HasKey(l => new { l.PostId, l.ApplicationUserId });

            builder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Like>()
                .HasOne(l => l.ApplicationUser)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //Comments
            builder.Entity<Comment>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                .HasOne(l => l.ApplicationUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(l => l.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //Favorites
            builder.Entity<Favorite>()
                .HasKey(f => new { f.PostId, f.ApplicationUserId });

            builder.Entity<Favorite>()
                .HasOne(f => f.Post)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Favorite>()
                .HasOne(f => f.ApplicationUser)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //Reports
            builder.Entity<Report>()
                .HasKey(f => new { f.PostId, f.ApplicationUserId });

            builder.Entity<Report>()
                .HasOne(f => f.Post)
                .WithMany(p => p.Reports)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Report>()
                .HasOne(f => f.ApplicationUser)
                .WithMany(u => u.Reports)
                .HasForeignKey(f => f.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
