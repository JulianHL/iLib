using iLib.Services;
using Microsoft.AspNetCore.Mvc;

namespace iLib.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserService _userService;

        public BaseController()
        {
            _userService = new UserService();
        }

        public Func<object, IActionResult> Router(string authorizedRole)
        {
            string? username = HttpContext.Session.GetString("UserName");
            if (username == null)
            {
                return (object o) => RedirectToAction("Login", "Login");
            }

            string userRole = _userService.GetUserRoleByUserName(username);

            if (userRole.Equals("Admin"))
            {
                return (object o) => View(o);
            }

            HttpContext.Session.GetString("UserName");
            if (userRole != authorizedRole)
            {
                return (object o) => RedirectToAction("Login", "Login");
            }
            return (object o) => View(o);
        }

    }
}
