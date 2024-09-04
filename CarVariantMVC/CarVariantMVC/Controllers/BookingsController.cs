using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarVariantMVC.Models; // Make sure to include your models namespace
using System.Linq;
using System.Threading.Tasks;



namespace CarVariantMVC.Controllers
{
    public class BookingsController : Controller
    {
        private readonly CarVariantTestContext _context; // Replace with your DbContext name

        public BookingsController(Models.CarVariantTestContext context)
        {
            _context = context;
        }

        // GET: Bookings
        //public async Task<IActionResult> Index()
        //{
        //    var bookings = await _context.Bookings
        //        .Include(b => b.User)
        //        .Include(b => b.Variant)
        //        .ThenInclude(v => v.Company) // Assuming Variant has a Company navigation property
        //        .ToListAsync();

        //    return View(bookings);
        //}

        // You can add other actions for Create, Edit, Delete, etc. here
        public IActionResult Index()
        {
            // Get the username from cookies
            var username = Request.Cookies["Username"];

            // Check if the user is an admin
            if (username == "admin")
            {
                // Return all bookings for admin
                var allBookings = _context.Bookings.Include(b => b.User).Include(b => b.Variant).ThenInclude(v=>v.Company).ToList();
                return View(allBookings);
            }
            else
            {
                // Return bookings for the normal user
                var userId = int.Parse(Request.Cookies["UserId"]);
                var userBookings = _context.Bookings
                                           .Include(b => b.User)
                                           .Include(b => b.Variant)
                                           .ThenInclude(v=>v.Company)
                                           .Where(b => b.UserId == userId)
                                           .ToList();
                return View(userBookings);
            }
        }
        public IActionResult ReturnCar()
        {
            // Retrieve the user ID from the cookie
            int userId = Convert.ToInt32(Request.Cookies["UserId"]);

            // Get bookings for the logged-in user where the associated variant is not available
            var bookings = _context.Bookings
                                   .Include(b => b.Variant) // Include variant details
                                   .Where(b => b.UserId == userId && b.Variant.Status == "not available") // Filter for unavailable variants
                                   .ToList();

            return View(bookings);
        }
        [HttpPost]
        public IActionResult ConfirmReturn(int bookingId)
        {
            // Retrieve the booking based on the selected ID
            var booking = _context.Bookings.Find(bookingId);

            if (booking != null)
            {
                // Set the return date to now (or any specific logic you want)
                //booking.ReturnDate = DateTime.UtcNow;

                // Update the booking in the database
                _context.Bookings.Update(booking);
                _context.SaveChanges();

                // Optionally: update the variant's status to available
                var variant = _context.Variants.Find(booking.VariantId);
                if (variant != null)
                {
                    variant.Status = "available"; // Assuming "available" is the status you want to set
                    _context.Variants.Update(variant);
                    _context.SaveChanges();
                }

                return RedirectToAction("Index", "Bookings"); // Redirect to bookings list or any other page
            }

            // Handle case where booking was not found
            return RedirectToAction("ReturnCar"); // Redirect back to return car page
        }



    }
}
