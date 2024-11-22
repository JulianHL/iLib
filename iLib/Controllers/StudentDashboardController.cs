using iLib.Services;
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

        public IActionResult StudentBooks(int user_Id)
        {
            try
            {
                return View(_studentBookService.GetAllStudentBooksByStudentId(user_Id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }

        }
    }
}
