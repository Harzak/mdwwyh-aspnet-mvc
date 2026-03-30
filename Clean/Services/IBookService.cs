using System;
using System.Collections.Generic;
using Clean.Domain;
using Common.Database.Entities;

namespace Clean.Services
{
    public interface IBookService : IDisposable
    {
        List<Book> GetAllBooks();

        List<Book> SearchBooks(string searchTerm);

        Book GetBookById(int id);

        List<Library> GetAllLibraries();

        LibraryStatistics GetLibraryStatistics();

        int CreateBook(Book book);

        void UpdateBook(Book book);

        void DeleteBook(int id);
    }
}
