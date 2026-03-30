using System;
using System.Collections.Generic;
using System.Linq;
using Clean.Domain;
using Clean.Models;
using Common.Database.Entities;

namespace Clean.Assemblers
{
    internal sealed class BookAssembler : IBookAssembler
    {
        public BookListItemViewModel ToListItem(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            return new BookListItemViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            };
        }

        public List<BookListItemViewModel> ToListItems(List<Book> books)
        {
            if (books == null)
            {
                throw new ArgumentNullException(nameof(books));
            }

            return books.Select(this.ToListItem).ToList();
        }

        public BookDetailViewModel ToDetail(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
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

        public BookFormViewModel ToFormForCreate(List<Library> libraries)
        {
            if (libraries == null)
            {
                throw new ArgumentNullException(nameof(libraries));
            }

            BookFormViewModel model = new BookFormViewModel
            {
                AvailableLibraries = this.ToLibrarySelectItems(libraries)
            };

            return model;
        }

        public BookFormViewModel ToFormForEdit(Book book, List<Library> libraries)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (libraries == null)
            {
                throw new ArgumentNullException(nameof(libraries));
            }

            return new BookFormViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Isbn = book.Isbn,
                Genre = book.Genre,
                PublishedYear = book.PublishedYear,
                LibraryId = book.LibraryId,
                AvailableLibraries = this.ToLibrarySelectItems(libraries)
            };
        }

        public List<LibrarySelectItemViewModel> ToLibrarySelectItems(List<Library> libraries)
        {
            if (libraries == null)
            {
                throw new ArgumentNullException(nameof(libraries));
            }

            return libraries.Select(l => new LibrarySelectItemViewModel
            {
                Id = l.Id,
                Name = l.Name
            }).ToList();
        }

        public LibrarySummaryViewModel ToSummary(LibraryStatistics statistics)
        {
            if (statistics == null)
            {
                throw new ArgumentNullException(nameof(statistics));
            }

            return new LibrarySummaryViewModel
            {
                TotalBooks = statistics.TotalBooks,
                TotalLibraries = statistics.TotalLibraries,
                TotalAuthors = statistics.TotalAuthors,
                TotalGenres = statistics.TotalGenres
            };
        }

        public Book ToDomain(BookFormViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new Book
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                Isbn = model.Isbn,
                Genre = model.Genre,
                PublishedYear = model.PublishedYear,
                LibraryId = model.LibraryId
            };
        }
    }
}
