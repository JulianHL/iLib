using iLib.Services;
using iLib.Models;
using Microsoft.AspNetCore.Mvc;

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
                if(!_studentBookService.AddStudentBooks(userId, bookIsbn))
                {
                    return "There was a problem";
                }

                return "You borrow this book successfuly";

            } catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
