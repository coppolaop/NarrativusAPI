namespace NarrativusAPI.Models
{
    public class Relationship
    {
        public int Id { get; set; }
        public String Type { get; set; }

        public int RelatedToId { get; set; }

        public Character RelatedTo { get; set; }
    }
}
