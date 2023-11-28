namespace NarrativusAPI.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public required String Name { get; set; }
        public String Description { get; set; }
        public List<Session> Sessions { get; set; }
        public List<Character> Stars { get; set; }
    }
}
