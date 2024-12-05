using iLib.Models;
using iLib.Services;
using Microsoft.AspNetCore.Mvc;

namespace iLib.Controllers
{
    public class AdminDashboardController : Controller
    {
        StudentService _studentService;
        LibrarianService _librarianService;
        public AdminDashboardController()
        {
            _studentService = new StudentService();
            _librarianService = new LibrarianService();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        public IActionResult AddStudentAction(Student student)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddStudent");
            }
            try
            {
                return Ok(_studentService.AddStudent(student));
            }catch (Exception ex)
            {
                return StatusCode(500, "InternalServerError: "+ex.Message);
            }
        }

        public IActionResult AddLibrarian()
        {
            return View();
        }

        public IActionResult AddLibrarianAction(Librarian librarian)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddLibrarian");
            }
            try
            {
                return Ok(_librarianService.AddLibrarian(librarian));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "InternalServerError: " + ex.Message);
            }
        }
    }
}
