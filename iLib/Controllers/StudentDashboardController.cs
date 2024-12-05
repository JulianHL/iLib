using iLib.Services;
using iLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace iLib.Controllers
{
    public class StudentDashboardController : Controller
    {
        private StudentBookService _studentBookService;
        private StudentBookService _bookService;

        public StudentDashboardController()
        {
            _studentBookService = new StudentBookService();
            _bookService = new StudentBookService();
        }

        public IActionResult Index()
        {
            try
            {
                return View(_bookService.GetBooksByFaculty("Science"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                List<Book> emptyList = new List<Book>();
                return View(emptyList);
            }
        }

        public IActionResult SearchBooks(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    throw new ArgumentException("Search term cannot be null or empty", nameof(searchTerm));
                }
                var books = _studentBookService.SearchStudentBooks(searchTerm);
                return View(books);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View("Index");
            }
        }

    public IActionResult StudentBooks(int userId)
        {
            try
            {
                return View(_studentBookService.GetAllStudentBooksByStudentId(userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View("Index");
            }
        }

        public IActionResult AddStudentBook(int userId, string bookIsbn)
        {
            try
            {
                _studentBookService.AddStudentBooks(userId, bookIsbn);
                return RedirectToAction("Index");

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }

        public IActionResult BookDetails(string bookIsbn)
        {
            try
            {
                return View(_bookService.GetBookByIsbn(bookIsbn));

            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }

        public IActionResult StudentBookDetails(int userId, string bookIsbn)
        {
            try
            {
                return View(_studentBookService.GetStudentBookByIsbn(userId, bookIsbn));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }
    }
}
