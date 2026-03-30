using System.Collections.Generic;
using Clean.Domain;
using Clean.Models;
using Common.Database.Entities;

namespace Clean.Assemblers
{
    public interface IBookAssembler
    {
        BookListItemViewModel ToListItem(Book book);

        List<BookListItemViewModel> ToListItems(List<Book> books);

        BookDetailViewModel ToDetail(Book book);

        BookFormViewModel ToFormForCreate(List<Library> libraries);

        BookFormViewModel ToFormForEdit(Book book, List<Library> libraries);

        List<LibrarySelectItemViewModel> ToLibrarySelectItems(List<Library> libraries);

        LibrarySummaryViewModel ToSummary(LibraryStatistics statistics);

        Book ToDomain(BookFormViewModel model);
    }
}
