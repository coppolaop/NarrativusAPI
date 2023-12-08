namespace NarrativusAPI.Models
{
    public class Character
    {
        public int Id { get; set; }
        public required String Name { get; set; }
        public String Ancestry { get; set; }
        public String Family { get; set; }
        public String Sex { get; set; }
        public String PhysicalCharacteristics { get; set; }
        public String Background { get; set; }
        public String Roleplay { get; set; }
        public String DateOfBirth { get; set; }
        public String DateOfDeath { get; set; }

        public List<Relationship> Relationships { get; set; }
        public List<Appearance> Appearances { get; set; }

        public int? BelongsToId { get; set; }
        public Location? BelongsTo { get; set; }
    }
}
