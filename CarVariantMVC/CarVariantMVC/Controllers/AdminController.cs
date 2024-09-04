using CarVariantMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarVariantMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly CarVariantTestContext _context;

        public AdminController(CarVariantTestContext context)
        {
            _context = context;
        }
        //[Authorize(Roles ="Admin")]
        public IActionResult AdminDashboard()
        {
            var username = HttpContext.Request.Cookies["Username"];
            if (username != "admin")
            {
                return NotFound();
            }

            var model = new AdminDashboardViewModel
                {
                    CompanyCount = _context.Companies.Count(),
                    VariantCount = _context.Variants.Count(),
                    CustomerCount = _context.Users.Count(),
                    BookingCount = _context.Bookings.Count()
                };

                return View(model);  
            
            
        }
    }
}
