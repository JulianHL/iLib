using iLib.Models;
using iLib.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace iLib.Controllers
{
    public class StudentBooksController : Controller
    {
            private readonly ILogger<StudentBooksController> _logger;

            public StudentBooksController(ILogger<StudentBooksController> logger)
            {
                _logger = logger;
            }
            // Test Git JIRA
            public IActionResult Index()
            {
            try
            {
                StudentBookService studentBookService = new StudentBookService();
                return View(studentBookService.GetAllStudentBooksByStudentId(4000));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View("Home/Index");
            }
               
            }



            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
}
