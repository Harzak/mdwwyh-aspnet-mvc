using System.Collections.Generic;

namespace Clean.Models
{
    public sealed class BookIndexViewModel
    {
        public List<BookListItemViewModel> Books { get; set; }

        public LibrarySummaryViewModel Summary { get; set; }

        public BookIndexViewModel()
        {
            this.Books = new List<BookListItemViewModel>();
        }
    }
}
