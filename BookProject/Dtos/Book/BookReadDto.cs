namespace BookProject.Dtos.Book
{
    public class BookReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        public int Pages { get; set; }

        public bool Available { get; set; }

        public int? AuthorId { get; set; }
    }
}
