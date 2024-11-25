using iLib.Services;
using iLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace iLib.Controllers
{
    public class StudentDashboardController : Controller
    {
        StudentBookService _studentBookService;

        public StudentDashboardController()
        {
            _studentBookService = new StudentBookService();
        }

        public IActionResult Index()
        {
            return View();
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

        public string AddStudentBook(int userId, string bookIsbn)
        {
            try
            {
                string response = _studentBookService.AddStudentBooks(userId, bookIsbn);
                Console.WriteLine(response);
                return response;

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }
    }
}
