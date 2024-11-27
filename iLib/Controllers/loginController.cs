using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iLib.Models;
using iLib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace iLib.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LoginController> _logger;

        public LoginController(AppDbContext context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ViewBag.ErrorMessage = "Username and password are required.";
                return View();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_UserName == Username && u.User_Password == Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.User_Id.ToString());
                HttpContext.Session.SetString("UserName", user.User_UserName);
                HttpContext.Session.SetInt32("UserRole", user.User_Role);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }
    }
}
