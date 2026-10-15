using System.Text.Json.Serialization;

namespace BookProject.Models
{
    public class Authorwoof
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; } = string.Empty;
        public bool Active { get; set; } = true;

        // EF specifikus property-k
        [JsonIgnore]
        public IEnumerable<Book>? Books { get; set; } // Navigációs property

    }
}
