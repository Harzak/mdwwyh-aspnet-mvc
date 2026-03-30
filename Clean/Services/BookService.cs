using System;
using System.Collections.Generic;
using Clean.Domain;
using Common.Database.Entities;
using Common.Repository;

namespace Clean.Services
{
    internal sealed class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private bool _disposed;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            return _bookRepository.Search(searchTerm);
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public List<Library> GetAllLibraries()
        {
            return _bookRepository.GetAllLibraries();
        }

        public LibraryStatistics GetLibraryStatistics()
        {
            return new LibraryStatistics(
                _bookRepository.CountBooks(),
                _bookRepository.CountLibraries(),
                _bookRepository.CountDistinctAuthors(),
                _bookRepository.CountDistinctGenres()
            );
        }

        public int CreateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _bookRepository.Add(book);
            return book.Id;
        }

        public void UpdateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _bookRepository.Update(book);
        }

        public void DeleteBook(int id)
        {
            _bookRepository.Delete(id);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _bookRepository.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
