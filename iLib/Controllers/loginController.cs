﻿using Microsoft.AspNetCore.Mvc;
using iLib.Services;
using iLib.Models;


namespace iLib.Controllers
{
    public class LoginController : Controller
    {
        

        UserService _userService;

        public LoginController(ILogger<LoginController> logger)
        {
            _userService = new UserService();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserValidation(string username, string password)
        {
            try
            {
                User user = _userService.ValidateUser(username, password);
                HttpContext.Session.SetString("UserName", user.UserName);
                if (user.UserRole.Equals("Admin"))
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }
                if (user.UserRole.Equals("Librarian"))
                {
                    return RedirectToAction("Index", "LibrarianDashboard");
                }
                return RedirectToAction("Index", "StudentDashboard");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login");
            }
        }
    }
}
