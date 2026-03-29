namespace Clean.Models
{
    public sealed class BookDetailViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Isbn { get; set; }

        public string Genre { get; set; }

        public int? PublishedYear { get; set; }

        public string LibraryName { get; set; }
    }
}
