using System.Text.Json.Serialization;

namespace BookProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "nigger";
        public string Category { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        public int Pages { get; set; }

        public bool Available { get; set; }

        public int? AuthorId { get; set; }
        public bool Active { get; set; } = true;

        //Navigation property
        [JsonIgnore]
        public Authorwoof? Author {  get; set; }

    }
}
