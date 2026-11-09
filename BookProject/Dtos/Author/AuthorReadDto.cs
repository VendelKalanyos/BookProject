using System.Text.Json.Serialization;

namespace BookProject.Dtos.Author
{
    public class AuthorReadDto
    {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime DateOfBirth { get; set; }
            public string PlaceOfBirth { get; set; } = string.Empty;
            public string BirthDateAndPlace { get; set; } = string.Empty;
    }
}
