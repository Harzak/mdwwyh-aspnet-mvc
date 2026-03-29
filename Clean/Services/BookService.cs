using System;
using System.Collections.Generic;
using System.Linq;
using Clean.Models;
using Clean.Repositories;
using Common.Database.Entities;

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

        public List<BookListItemViewModel> GetAllBooks()
        {
            List<Book> books = _bookRepository.GetAll();
            return books.Select(MapToListItem).ToList();
        }

        public List<BookListItemViewModel> SearchBooks(string searchTerm)
        {
            List<Book> books = _bookRepository.Search(searchTerm);
            return books.Select(MapToListItem).ToList();
        }

        public BookDetailViewModel GetBookDetail(int id)
        {
            Book book = _bookRepository.GetById(id);
            if (book == null)
            {
                return null;
            }

            return new BookDetailViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Isbn = book.Isbn,
                Genre = book.Genre,
                PublishedYear = book.PublishedYear,
                LibraryName = book.Library != null ? book.Library.Name : string.Empty
            };
        }

        public BookFormViewModel GetBookFormForCreate()
        {
            BookFormViewModel model = new BookFormViewModel();
            this.PopulateLibraries(model);
            return model;
        }

        public BookFormViewModel GetBookFormForEdit(int id)
        {
            Book book = _bookRepository.GetById(id);
            if (book == null)
            {
                return null;
            }

            BookFormViewModel model = new BookFormViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Isbn = book.Isbn,
                Genre = book.Genre,
                PublishedYear = book.PublishedYear,
                LibraryId = book.LibraryId
            };

            this.PopulateLibraries(model);
            return model;
        }

        public int CreateBook(BookFormViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Book book = new Book
            {
                Title = model.Title,
                Author = model.Author,
                Isbn = model.Isbn,
                Genre = model.Genre,
                PublishedYear = model.PublishedYear,
                LibraryId = model.LibraryId
            };

            _bookRepository.Add(book);
            return book.Id;
        }

        public void UpdateBook(BookFormViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Book book = new Book
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                Isbn = model.Isbn,
                Genre = model.Genre,
                PublishedYear = model.PublishedYear,
                LibraryId = model.LibraryId
            };

            _bookRepository.Update(book);
        }

        public void DeleteBook(int id)
        {
            _bookRepository.Delete(id);
        }

        public LibrarySummaryViewModel GetLibrarySummary()
        {
            return new LibrarySummaryViewModel
            {
                TotalBooks = _bookRepository.CountBooks(),
                TotalLibraries = _bookRepository.CountLibraries(),
                TotalAuthors = _bookRepository.CountDistinctAuthors(),
                TotalGenres = _bookRepository.CountDistinctGenres()
            };
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

        private void PopulateLibraries(BookFormViewModel model)
        {
            List<Library> libraries = _bookRepository.GetAllLibraries();
            model.AvailableLibraries = libraries.Select(l => new LibrarySelectItemViewModel
            {
                Id = l.Id,
                Name = l.Name
            }).ToList();
        }

        private static BookListItemViewModel MapToListItem(Book book)
        {
            return new BookListItemViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            };
        }
    }
}
