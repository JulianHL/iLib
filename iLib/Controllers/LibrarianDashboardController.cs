using iLib.Models;
using iLib.Services;
using Microsoft.AspNetCore.Mvc;

namespace iLib.Controllers
{
    public class LibrarianDashboardController : BaseController
    {
        private readonly StudentBookService _studentBookService;
        private readonly BookService _bookService;
        private readonly UserService _userService;

        public LibrarianDashboardController()
        {
            _studentBookService = new StudentBookService();
            _bookService = new BookService();
            _userService = new UserService();
        }

        public IActionResult Index()
        {

            return Router("Librarian").Invoke(_studentBookService.GetAllBooks());
        }

        public IActionResult StudentsBooks()
        {
            try
            {

                return Router("Librarian").Invoke(_studentBookService.GetAllStudentBooks());
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
                Func<object, IActionResult> router = Router("Student");

                string? username = HttpContext.Session.GetString("UserName");
                if (username == null)
                {
                    return router.Invoke("");
                }

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
