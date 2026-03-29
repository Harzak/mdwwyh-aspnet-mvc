using System;
using System.Collections.Generic;
using Clean.Models;

namespace Clean.Services
{
    public interface IBookService : IDisposable
    {
        List<BookListItemViewModel> GetAllBooks();

        List<BookListItemViewModel> SearchBooks(string searchTerm);

        BookDetailViewModel GetBookDetail(int id);

        BookFormViewModel GetBookFormForCreate();

        BookFormViewModel GetBookFormForEdit(int id);

        int CreateBook(BookFormViewModel model);

        void UpdateBook(BookFormViewModel model);

        void DeleteBook(int id);

        LibrarySummaryViewModel GetLibrarySummary();
    }
}
