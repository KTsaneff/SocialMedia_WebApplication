using System.ComponentModel.DataAnnotations;

namespace LoopSocialApp.Data.DataModels
{
    public class Story
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public bool IsDeleted { get; set; }

        public string ApplicationUserId { get; set; } = null!;

        //Navigation properties
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
