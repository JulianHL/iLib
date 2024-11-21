using iLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace iLib.Controllers
{
    public class AccountController : Controller
    {
  
        public ActionResult Login()
        {
            return View();
        }
    }
}
