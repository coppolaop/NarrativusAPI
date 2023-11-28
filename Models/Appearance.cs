namespace NarrativusAPI.Models
{
    public class Appearance
    {
        public int Id { get; set; }
        public string Reason { get; set; }

        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
