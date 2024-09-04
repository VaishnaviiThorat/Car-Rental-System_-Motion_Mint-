using Microsoft.AspNetCore.Http;
using CarVariantMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarVariantMVC.Controllers
{
    public class AccountController : Controller
    {
        private  readonly CarVariantTestContext _context;
        public AccountController(CarVariantTestContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        // Register POST method
        [HttpPost]
        public IActionResult Register(UserRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = model.Password, // Store the password directly (no hashing)
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                // Redirect to login or another page
                return RedirectToAction("Login");
            }

            // If the model state is invalid, return the view with the current model to show validation errors
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
       
        public IActionResult Login(LoginViewModel model)
        {
            // Validate the user credentials
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.PasswordHash == model.Password);
            if (user != null)
            {
                // Set cookies for Username and UserId
                Response.Cookies.Append("Username", user.Username, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) });
                Response.Cookies.Append("UserId", user.Id.ToString(), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) });

                // Redirect to UserDashboard
                if (user.Username == "admin") {


                     return RedirectToAction("AdminDashboard", "Admin");
                }

                else
                {
                    return RedirectToAction("UserDashboard", "User");
                }

            }
            // Handle login failure
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }



        // Logout method
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("UserId");
            // Clear session or cookies
            return RedirectToAction("Login");
        }
    }






}
