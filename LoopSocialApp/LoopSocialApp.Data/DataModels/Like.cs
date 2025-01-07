namespace LoopSocialApp.Data.DataModels
{
    public class Like
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string ApplicationUserId { get; set; } = null!;


        //Navigation properties
        public Post Post { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
