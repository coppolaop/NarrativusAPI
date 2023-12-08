namespace NarrativusAPI.DTOs
{
    public class RelationshipDTO
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int RelatedToId { get; set; }
        public string Relation { get; set; }
    }
}
