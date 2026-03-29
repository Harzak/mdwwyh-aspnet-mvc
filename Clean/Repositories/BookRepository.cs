using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Database;
using Common.Database.Entities;

namespace Clean.Repositories
{
    internal sealed class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        private bool _disposed;

        public BookRepository(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.Library)
                .OrderBy(b => b.Title)
                .ToList();
        }

        public List<Book> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return this.GetAll();
            }

            string term = searchTerm.Trim().ToLower();

            return _context.Books
                .Include(b => b.Library)
                .Where(b => b.Title.ToLower().Contains(term)
                         || b.Author.ToLower().Contains(term)
                         || (b.Isbn != null && b.Isbn.ToLower().Contains(term)))
                .OrderBy(b => b.Title)
                .ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books
                .Include(b => b.Library)
                .FirstOrDefault(b => b.Id == id);
        }

        public void Add(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            Book existing = _context.Books.Find(book.Id);
            if (existing == null)
            {
                throw new InvalidOperationException($"Book with Id {book.Id} not found.");
            }

            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Isbn = book.Isbn;
            existing.Genre = book.Genre;
            existing.PublishedYear = book.PublishedYear;
            existing.LibraryId = book.LibraryId;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                throw new InvalidOperationException($"Book with Id {id} not found.");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public int CountBooks()
        {
            return _context.Books.Count();
        }

        public int CountLibraries()
        {
            return _context.Libraries.Count();
        }

        public int CountDistinctAuthors()
        {
            return _context.Books
                .Select(b => b.Author)
                .Distinct()
                .Count();
        }

        public int CountDistinctGenres()
        {
            return _context.Books
                .Where(b => b.Genre != null)
                .Select(b => b.Genre)
                .Distinct()
                .Count();
        }

        public List<Library> GetAllLibraries()
        {
            return _context.Libraries
                .OrderBy(l => l.Name)
                .ToList();
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
                    _context.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
