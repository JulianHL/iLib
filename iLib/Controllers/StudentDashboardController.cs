using iLib.Services;
using Microsoft.AspNetCore.Mvc;
using iLib.Exceptions;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;

namespace iLib.Controllers
{
    public class StudentDashboardController : BaseController
    {
        readonly StudentBookService _studentBookService;
        readonly StudentBookService _bookService;
        readonly UserService _userService;
        readonly StudentService _studentService;

        public StudentDashboardController()
        {
            _studentBookService = new StudentBookService();
            _bookService = new StudentBookService();
            _userService = new UserService();
            _studentService = new StudentService();
        }

  

        public IActionResult Index()
        {
            try
            {
                Func<object, IActionResult> router = Router("Student");

                string? username = HttpContext.Session.GetString("UserName");
                if(username == null)
                {
                    return router.Invoke("");
                }

                string faculty = _studentService.GetStudentFacultyByUserName(username);
                return router.Invoke(_bookService.GetBooksByFaculty(faculty));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: "Internal Server Error", detail: ex.Message);
            }
        }

        public IActionResult StudentBooks()
        {
            try
            {

                Func<object, IActionResult> router = Router("Student");

                string? username = HttpContext.Session.GetString("UserName");
                if (username == null)
                {
                    return router.Invoke("");
                }

                int userId = _userService.GetUserIdByUserName(username);
                return Router("Student").Invoke(_studentBookService.GetAllStudentBooksByStudentId(userId));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: "Internal Server Error", detail: ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddStudentBook([FromBody] JsonElement json)
        {
            try
            {
                if (!json.TryGetProperty("bookIsbn", out JsonElement bookIsbnJsonElement) || bookIsbnJsonElement.ValueKind != JsonValueKind.String)
                {
                    throw new BadRequestException("Invalid Isbn");
                }

                string? bookIsbn = bookIsbnJsonElement.GetString();
                if (bookIsbn == null)
                {
                    throw new BadRequestException("Isbn is null");
                }

                Func<object, IActionResult> router = Router("Student");

                string? username = HttpContext.Session.GetString("UserName");
                if (username == null)
                {
                    return router.Invoke("");
                }

                int userId = _userService.GetUserIdByUserName(username);

                return Ok(_studentBookService.AddStudentBooks(userId, bookIsbn));

            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }

        public IActionResult BookDetails(string bookIsbn)
        {
            try
            {

                return Router("Student").Invoke(_bookService.GetBookByIsbn(bookIsbn));

            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: "Internal Server Error", detail: ex.Message);
            }
        }

        public IActionResult StudentBookDetails( string bookIsbn)
        {
            try
            {
                Func<object, IActionResult> router = Router("Student");

                string? username = HttpContext.Session.GetString("UserName");
                if (username == null)
                {
                    return router.Invoke("");
                }

                int userId = _userService.GetUserIdByUserName(username);
                return Router("Student").Invoke(_studentBookService.GetStudentBookByIsbn(userId, bookIsbn));

            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: "Internal Server Error", detail: ex.Message);
            }
        }
    }
}
