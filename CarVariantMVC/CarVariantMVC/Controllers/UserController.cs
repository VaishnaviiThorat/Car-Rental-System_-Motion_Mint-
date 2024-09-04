using Microsoft.AspNetCore.Mvc;

namespace CarVariantMVC.Controllers
{
    public class UserController : Controller
    {
        //public IActionResult UserDashboard()
        //{
        //    return RedirectToAction("AvailableCars", "AvailableCars");
        //}
        public IActionResult UserDashboard()
        {
            // Get values from cookies
            var username = Request.Cookies["Username"];
            var userIdString = Request.Cookies["UserId"];
            int userId = int.Parse(userIdString); // Convert string to int

            // Redirect to AvailableCars action, passing userId and username
            return RedirectToAction("AvailableCars", "AvailableCars", new { userId = userId, username = username });
        }

    }
}
