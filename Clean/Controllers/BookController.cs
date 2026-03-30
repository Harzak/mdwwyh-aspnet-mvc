using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Clean.Assemblers;
using Clean.Domain;
using Clean.Models;
using Clean.Services;
using Common.Database.Entities;

namespace Clean.Controllers
{
    [Authorize]
    public sealed class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBookAssembler _bookAssembler;

        public BookController(IBookService bookService, IBookAssembler bookAssembler)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _bookAssembler = bookAssembler ?? throw new ArgumentNullException(nameof(bookAssembler));
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Book> books = _bookService.GetAllBooks();
            LibraryStatistics statistics = _bookService.GetLibraryStatistics();

            BookIndexViewModel model = new BookIndexViewModel
            {
                Books = _bookAssembler.ToListItems(books),
                Summary = _bookAssembler.ToSummary(statistics)
            };

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Search(string term)
        {
            List<Book> books = _bookService.SearchBooks(term);
            List<BookListItemViewModel> listItems = _bookAssembler.ToListItems(books);
            return this.PartialView("_BookList", listItems);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            Book book = _bookService.GetBookById(id);
            if (book == null)
            {
                return this.HttpNotFound();
            }

            BookDetailViewModel model = _bookAssembler.ToDetail(book);
            return this.PartialView("_BookDetail", model);
        }

        [HttpGet]
        public ActionResult Summary()
        {
            LibraryStatistics statistics = _bookService.GetLibraryStatistics();
            LibrarySummaryViewModel model = _bookAssembler.ToSummary(statistics);
            return this.PartialView("_LibrarySummary", model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<Library> libraries = _bookService.GetAllLibraries();
            BookFormViewModel model = _bookAssembler.ToFormForCreate(libraries);
            return this.PartialView("_BookForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                List<Library> libraries = _bookService.GetAllLibraries();
                model.AvailableLibraries = _bookAssembler.ToLibrarySelectItems(libraries);
                return this.PartialView("_BookForm", model);
            }

            Book book = _bookAssembler.ToDomain(model);
            int bookId = _bookService.CreateBook(book);
            return this.Json(new { success = true, bookId });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book book = _bookService.GetBookById(id);
            if (book == null)
            {
                return this.HttpNotFound();
            }

            List<Library> libraries = _bookService.GetAllLibraries();
            BookFormViewModel model = _bookAssembler.ToFormForEdit(book, libraries);
            return this.PartialView("_BookForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                List<Library> libraries = _bookService.GetAllLibraries();
                model.AvailableLibraries = _bookAssembler.ToLibrarySelectItems(libraries);
                return this.PartialView("_BookForm", model);
            }

            Book book = _bookAssembler.ToDomain(model);
            _bookService.UpdateBook(book);
            return this.Json(new { success = true, bookId = model.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return this.Json(new { success = true });
        }
    }
}
