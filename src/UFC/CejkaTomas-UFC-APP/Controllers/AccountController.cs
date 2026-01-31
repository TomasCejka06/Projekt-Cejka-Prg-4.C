using CejkaTomas_UFC_APP.Data;
using CejkaTomas_UFC_APP.Models;
using CejkaTomas_UFC_APP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CejkaTomas_UFC_APP.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // unikátnost username/email
            bool usernameExists = await _context.Users.AnyAsync(u => u.Username == vm.Username);
            if (usernameExists)
            {
                ModelState.AddModelError(nameof(vm.Username), "Username is already taken.");
                return View(vm);
            }

            if (!string.IsNullOrWhiteSpace(vm.Email))
            {
                bool emailExists = await _context.Users.AnyAsync(u => u.Email == vm.Email);
                if (emailExists)
                {
                    ModelState.AddModelError(nameof(vm.Email), "Email is already used.");
                    return View(vm);
                }
            }

            var user = new User
            {
                Username = vm.Username,
                Email = string.IsNullOrWhiteSpace(vm.Email) ? null : vm.Email,
                PasswordHash = HashPassword(vm.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // zatím jen přesměruj na Login 
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            
            
            return View(vm);
        }

        
        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }
    }
}
