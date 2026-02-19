using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS_2026.Models;
using TMS_Repository.Data;

namespace TMS_2026.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users
                .FirstOrDefault(x => x.Email == model.Email && x.PasswordHash == model.PasswordHash);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);
            if (user.Role == "Admin")
                return RedirectToAction("TaskList", "Home");

            if (user.Role == "Manager")
                return RedirectToAction("TaskList", "Home");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
