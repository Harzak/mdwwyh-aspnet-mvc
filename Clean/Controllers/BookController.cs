using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Clean.Models;
using Common.Repository;
using Clean.Services;
using Common.Database;

namespace Clean.Controllers
{
    [Authorize]
    public sealed class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController()
            : this(new BookService(new BookRepository(new LibraryDbContext())))
        {
        }

        public BookController(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [HttpGet]
        public ActionResult Index()
        {
            BookIndexViewModel model = new BookIndexViewModel
            {
                Books = _bookService.GetAllBooks(),
                Summary = _bookService.GetLibrarySummary()
            };

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Search(string term)
        {
            List<BookListItemViewModel> books = _bookService.SearchBooks(term);
            return this.PartialView("_BookList", books);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            BookDetailViewModel model = _bookService.GetBookDetail(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.PartialView("_BookDetail", model);
        }

        [HttpGet]
        public ActionResult Summary()
        {
            LibrarySummaryViewModel model = _bookService.GetLibrarySummary();
            return this.PartialView("_LibrarySummary", model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            BookFormViewModel model = _bookService.GetBookFormForCreate();
            return this.PartialView("_BookForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                BookFormViewModel refreshedModel = _bookService.GetBookFormForCreate();
                refreshedModel.Title = model.Title;
                refreshedModel.Author = model.Author;
                refreshedModel.Isbn = model.Isbn;
                refreshedModel.Genre = model.Genre;
                refreshedModel.PublishedYear = model.PublishedYear;
                refreshedModel.LibraryId = model.LibraryId;
                return this.PartialView("_BookForm", refreshedModel);
            }

            int bookId = _bookService.CreateBook(model);
            return this.Json(new { success = true, bookId });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            BookFormViewModel model = _bookService.GetBookFormForEdit(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.PartialView("_BookForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                BookFormViewModel refreshedModel = _bookService.GetBookFormForEdit(model.Id);
                if (refreshedModel == null)
                {
                    return this.HttpNotFound();
                }

                refreshedModel.Title = model.Title;
                refreshedModel.Author = model.Author;
                refreshedModel.Isbn = model.Isbn;
                refreshedModel.Genre = model.Genre;
                refreshedModel.PublishedYear = model.PublishedYear;
                refreshedModel.LibraryId = model.LibraryId;
                return this.PartialView("_BookForm", refreshedModel);
            }

            _bookService.UpdateBook(model);
            return this.Json(new { success = true, bookId = model.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return this.Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bookService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
