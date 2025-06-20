namespace LoopSocialApp.Data.DataModels
{
    public class Report
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public int? PostId { get; set; }

        public int? StoryId { get; set; }

        public string ApplicationUserId { get; set; } = null!;


        //Navigation properties
        public Post? Post { get; set; }
        public Story? Story { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
