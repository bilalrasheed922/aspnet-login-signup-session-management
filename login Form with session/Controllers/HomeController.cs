using System.Diagnostics;
using login_Form_with_session.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace login_Form_with_session.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoginwithsessionContext context;

        public HomeController(LoginwithsessionContext context)
        {
            this.context = context;
        }

        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            // Check if the user exists in the database
            var userData = context.Users
                .FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (userData != null)
            {
                // Store user info in session
                HttpContext.Session.SetString("UserName", userData.Name);
                HttpContext.Session.SetString("UserId", userData.Email);

                // Redirect to home page after login
                return RedirectToAction("Dashboard");
            }
            else
            {
                // Show error message if login fails
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View();
            }
        }
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User newUser)
        {
            if (ModelState.IsValid)
            {
                // Save user to database
                context.Users.Add(newUser);
                context.SaveChanges();

                // Optional: Auto login after signup
                HttpContext.Session.SetString("UserName", newUser.Name);
                HttpContext.Session.SetString("UserId", newUser.Email);

                return RedirectToAction("Dashboard");
            }

            return View(newUser);
        }

        public IActionResult Dashboard()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userId = HttpContext.Session.GetString("UserId");

            if (userName != null)
            {
                ViewBag.UserName = userName;
                ViewBag.UserId = userId;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
       

        
        
    }
}
