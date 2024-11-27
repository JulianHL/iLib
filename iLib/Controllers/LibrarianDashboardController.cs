using iLib.Models;
using iLib.Services;
using Microsoft.AspNetCore.Mvc;

namespace iLib.Controllers
{
    public class LibrarianDashboardController : Controller
    {
        private readonly LibrarianBookService _librarianBookService;

        public LibrarianDashboardController()
        {
            _librarianBookService = new LibrarianBookService();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllBooks()
        {
            try
            {
                return View(_librarianBookService.GetAllBooks());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View("Index");
            }
        }

        public IActionResult BorrowedBooks()
        {
            try
            {
                return View(_librarianBookService.GetBorrowedBooks());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View("Index");
            }
        }
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBookToDB(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            try
            {
                string response = _librarianBookService.AddBook(book);
                TempData["Message"] = response;
                return RedirectToAction("AllBooks");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(book);
            }
        }

        [HttpPost]
        public string UpdateBook([FromBody] Book book)
        {
            try
            {
                string response = _librarianBookService.UpdateBook(book);
                Console.WriteLine(response);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }
    }
}
