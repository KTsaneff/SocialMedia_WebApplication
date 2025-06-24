using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopSocialApp.Data.DataModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;

        public string? ProfileImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        //Navigation properties
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public virtual ICollection<Story> Stories { get; set; } = new List<Story>();

        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

        public virtual ICollection<Story> Stories { get; set; } = new List<Story>();
    }
}
