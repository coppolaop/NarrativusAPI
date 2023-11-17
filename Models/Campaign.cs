namespace NarrativusAPI.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public required String Name { get; set; }
        public String Description { get; set; }
        public String Date { get; set; }
    }
}
