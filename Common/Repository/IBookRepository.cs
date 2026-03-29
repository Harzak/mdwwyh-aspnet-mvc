using System;
using System.Collections.Generic;
using Common.Database.Entities;

namespace Common.Repository
{
    public interface IBookRepository : IDisposable
    {
        List<Book> GetAll();

        List<Book> Search(string searchTerm);

        Book GetById(int id);

        void Add(Book book);

        void Update(Book book);

        void Delete(int id);

        int CountBooks();

        int CountLibraries();

        int CountDistinctAuthors();

        int CountDistinctGenres();

        List<Library> GetAllLibraries();
    }
}
