using iLib.Models;
using iLib.Services;
using Microsoft.AspNetCore.Mvc;

namespace iLib.Controllers
{
    public class LibrarianDashboardController : Controller
    {
        private readonly StudentBookService _studentBookService;
        private readonly BookService _bookService;

        public LibrarianDashboardController()
        {
            _studentBookService = new StudentBookService();
            _bookService = new BookService();
        }

        public IActionResult Index()
        {
            return View(_studentBookService.GetAllBooks());
        }

        public IActionResult StudentsBooks()
        {
            try
            {
                return View(_studentBookService.GetAllStudentBooks());
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
        public IActionResult AddBookAction(Book book)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddBook");
            }
            try
            {
                string response = _bookService.AddBook(book);
                TempData["Message"] = response;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("AddBook");
            }
        }

        [HttpPost]
        public string UpdateBook([FromBody] Book book)
        {
            try
            {
                string response = _studentBookService.UpdateBook(book);
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
