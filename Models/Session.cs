namespace NarrativusAPI.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Season { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int CampaignId { get; set; }
        public Campaign? Campaign { get; set; }

        public List<Appearance> Appearances { get; set; }
    }
}
