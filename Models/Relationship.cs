namespace NarrativusAPI.Models
{
    public class Relationship
    {
        public int Id { get; set; }
        public String Type { get; set; }

        public int OwnerId { get; set; }

        public virtual Character Owner { get; set; }

        public int RelatedToId { get; set; }

        public virtual Character RelatedTo { get; set; }
    }
}
