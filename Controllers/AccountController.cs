using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Linq;
using VarelaCarWash.Models;

namespace VarelaCarWash.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        public IActionResult Register() => View(new RegisterViewModel());

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel input)
        {
            if (!ModelState.IsValid)
                return View(input);

            if (_context.Users.Any(u => u.Email == input.Email))
            {
                ModelState.AddModelError("", "El correo ya está registrado.");
                return View(input);
            }

            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                FullName = input.FullName,
                Email = input.Email,
                PasswordHash = hasher.HashPassword(null, input.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("UserId", user.Id);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost]
        public IActionResult Login(LoginViewModel input)
        {
            if (!ModelState.IsValid)
                return View(input);

            var user = _context.Users.FirstOrDefault(u => u.Email == input.Email);
            if (user != null)
            {
                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(user, user.PasswordHash, input.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.ErrorMessage = "Correo o contraseña incorrectos.";
            return View(input);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId"); 
            return RedirectToAction("Login", "Account");
        }

    }
}
