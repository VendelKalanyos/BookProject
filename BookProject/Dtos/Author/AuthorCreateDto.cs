namespace BookProject.Dtos.Author
{
    public class AuthorCreateDto
    {
            public string Name { get; set; } = string.Empty;
            public DateTime DateOfBirth { get; set; }
            public string PlaceOfBirth { get; set; } = string.Empty;
    }
}
