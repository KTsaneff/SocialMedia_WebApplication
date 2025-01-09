namespace LoopSocialApp.Data.DataModels
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }


        //Foreign keys
        public int PostId { get; set; }

        public string ApplicationUserId { get; set; } = null!;


        //Navigation properties
        public ApplicationUser ApplicationUser { get; set; }
        public Post Post { get; set; }
    }
}
