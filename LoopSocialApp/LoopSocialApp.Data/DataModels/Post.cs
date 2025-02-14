using System.ComponentModel.DataAnnotations;

namespace LoopSocialApp.Data.DataModels
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public int NumberOfReposrts { get; set; }

        public bool IsPrivate { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public string ApplicationUserId { get; set; } = null!;

        //Navigation properties
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
