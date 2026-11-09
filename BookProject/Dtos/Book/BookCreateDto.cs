namespace BookProject.Dtos.Book
{
    public class BookCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        public int Pages { get; set; }

        public bool Available { get; set; }

        public int? AuthorId { get; set; }
    }
}
