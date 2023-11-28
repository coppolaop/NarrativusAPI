namespace NarrativusAPI.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Season { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }

        public List<Appearance> Appearances { get; set; }
    }
}
