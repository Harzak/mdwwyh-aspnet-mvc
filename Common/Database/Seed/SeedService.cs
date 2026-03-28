using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Common.Database.Entities;
using Common.Database.Enums;

namespace Common.Database.Seed
{
    public sealed class SeedService : ISeedService
    {
        private readonly LibraryDbContext _context;

        public SeedService(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void SeedAll()
        {
            SeedLibraries();
            SeedBooks();
            SeedUsers();
            _context.SaveChanges();
        }

        private void SeedLibraries()
        {
            List<Library> libraries = new List<Library>
            {
                new Library
                {
                    Id = 1,
                    Name = "Central City Library",
                    Address = "100 Main Street, Central City",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Library
                {
                    Id = 2,
                    Name = "Westside Branch Library",
                    Address = "250 Oak Avenue, Westside",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            foreach (Library library in libraries)
            {
                _context.Libraries.AddOrUpdate(l => l.Name, library);
            }
        }

        private void SeedBooks()
        {
            List<Book> books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    LibraryId = 1,
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    Isbn = "9780132350884",
                    Genre = "Software Engineering",
                    PublishedYear = 2008,
                    CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
                },
                new Book
                {
                    Id = 2,
                    LibraryId = 1,
                    Title = "Domain-Driven Design",
                    Author = "Eric Evans",
                    Isbn = "9780321125217",
                    Genre = "Software Engineering",
                    PublishedYear = 2003,
                    CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
                },
                new Book
                {
                    Id = 3,
                    LibraryId = 1,
                    Title = "Design Patterns",
                    Author = "Erich Gamma",
                    Isbn = "9780201633610",
                    Genre = "Software Engineering",
                    PublishedYear = 1994,
                    CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
                },
                new Book
                {
                    Id = 4,
                    LibraryId = 1,
                    Title = "Refactoring",
                    Author = "Martin Fowler",
                    Isbn = "9780134757599",
                    Genre = "Software Engineering",
                    PublishedYear = 2018,
                    CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
                },
                new Book
                {
                    Id = 5,
                    LibraryId = 1,
                    Title = "The Pragmatic Programmer",
                    Author = "David Thomas",
                    Isbn = "9780135957059",
                    Genre = "Software Engineering",
                    PublishedYear = 2019,
                    CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
                },
                new Book
                {
                    Id = 6,
                    LibraryId = 2,
                    Title = "Implementing Domain-Driven Design",
                    Author = "Vaughn Vernon",
                    Isbn = "9780321834577",
                    Genre = "Software Engineering",
                    PublishedYear = 2013,
                    CreatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Book
                {
                    Id = 7,
                    LibraryId = 2,
                    Title = "Working Effectively with Legacy Code",
                    Author = "Michael Feathers",
                    Isbn = "9780131177055",
                    Genre = "Software Engineering",
                    PublishedYear = 2004,
                    CreatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Book
                {
                    Id = 8,
                    LibraryId = 2,
                    Title = "Patterns of Enterprise Application Architecture",
                    Author = "Martin Fowler",
                    Isbn = "9780321127426",
                    Genre = "Software Architecture",
                    PublishedYear = 2002,
                    CreatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Book
                {
                    Id = 9,
                    LibraryId = 2,
                    Title = "Continuous Delivery",
                    Author = "Jez Humble",
                    Isbn = "9780321601919",
                    Genre = "DevOps",
                    PublishedYear = 2010,
                    CreatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Book
                {
                    Id = 10,
                    LibraryId = 2,
                    Title = "The Art of Unit Testing",
                    Author = "Roy Osherove",
                    Isbn = "9781617290893",
                    Genre = "Software Testing",
                    PublishedYear = 2013,
                    CreatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            foreach (Book book in books)
            {
                _context.Books.AddOrUpdate(b => b.Isbn, book);
            }
        }

        private void SeedUsers()
        {
            List<User> users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@library.local",
                    DisplayName = "System Administrator",
                    Role = EUserRole.Admin,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 2,
                    Username = "librarian",
                    Email = "librarian@library.local",
                    DisplayName = "Jane Librarian",
                    Role = EUserRole.Librarian,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 3,
                    Username = "reader",
                    Email = "reader@library.local",
                    DisplayName = "John Reader",
                    Role = EUserRole.Reader,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            foreach (User user in users)
            {
                _context.Users.AddOrUpdate(u => u.Username, user);
            }
        }
    }
}
