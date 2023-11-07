using System.Text.Json.Serialization;

namespace NarrativusAPI.Models
{
    public class Location
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        public String Description { get; set; }
        public String FoundationDate { get; set; }

        public int? LocatedInId { get; set; }

        [JsonIgnore]
        public Location? LocatedIn { get; set; }
    }
}
