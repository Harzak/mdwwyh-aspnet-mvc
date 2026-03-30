namespace Clean.Domain
{
    public sealed class LibraryStatistics
    {
        public int TotalBooks { get; }

        public int TotalLibraries { get; }

        public int TotalAuthors { get; }

        public int TotalGenres { get; }

        public LibraryStatistics(int totalBooks, int totalLibraries, int totalAuthors, int totalGenres)
        {
            this.TotalBooks = totalBooks;
            this.TotalLibraries = totalLibraries;
            this.TotalAuthors = totalAuthors;
            this.TotalGenres = totalGenres;
        }
    }
}
