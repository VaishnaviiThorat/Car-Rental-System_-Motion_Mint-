using CarVariantMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarVariantMVC.Controllers
{
    public class AvailableCarsController : Controller
    {
        private readonly CarVariantTestContext _context;
        public AvailableCarsController(CarVariantTestContext context)
        {
            _context = context;
        }
        public IActionResult AvailableCars(int userId, string username)
        {
            var availableCars = _context.Variants
                                        .Include(v => v.Company)
                                        .Where(v => v.Status == "available")
                                        .ToList();
            ViewBag.Username = username;
            ViewBag.UserId = userId; 
            // Pass the UserId to the View
            return View(availableCars);
        }
        [HttpGet]
        public IActionResult RentCar(int id, int userId, string username)
        {



            // Retrieve the variant details
            var variant = _context.Variants
                                  .Include(v => v.Company)
                                  .FirstOrDefault(v => v.Id == id);

            if (variant == null || variant.Status != "available")
            {
                return NotFound(); // Handle case where the car variant is not found or not available
            }

            // Create the booking view model
            var bookingViewModel = new BookingViewModel
            {
                VariantId = variant.Id,
                VariantName = variant.Name,
                CompanyName = variant.Company.Name,
                UserId = userId,
                UserName = username,
                PricePerDay = variant.PricePerDay.GetValueOrDefault(0),
                BookingDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(1) // Default to 1-day rental, can be adjusted by the user
            };

            return View("BookingDetails", bookingViewModel); // Show the form for confirming the booking
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Calculate the total days between booking and return dates
                var totalDays = (model.ReturnDate - model.BookingDate)?.TotalDays;

                // Log the dates and totalDays for debugging
                Console.WriteLine($"BookingDate: {model.BookingDate}");
                Console.WriteLine($"ReturnDate: {model.ReturnDate}");
                Console.WriteLine($"TotalDays: {totalDays}");
                Console.WriteLine($"PricePerDay: {model.PricePerDay}");

                if (totalDays.HasValue)
                {
                    Console.WriteLine($"TotalDays (actual): {totalDays.Value}");
                }
                else
                {
                    Console.WriteLine("TotalDays is null or invalid.");
                }

                // Calculate the total amount
                var totalAmount = (totalDays.HasValue && totalDays > 0)
                    ? model.PricePerDay * (decimal)totalDays.Value
                    : 0;

                Console.WriteLine($"Calculated TotalAmount: {totalAmount}");

                var booking = new Booking
                {
                    VariantId = model.VariantId,
                    UserId = model.UserId,
                    BookingDate = model.BookingDate,
                    ReturnDate = model.ReturnDate,
                    TotalAmount = totalAmount // Assign the calculated total amount
                };

                var variant = await _context.Variants.FindAsync(model.VariantId);
                if (variant != null)
                {
                    variant.Status = "not available"; // Update variant status
                }

                _context.Bookings.Add(booking);
                _context.Variants.Update(variant);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Bookings"); // Redirect to a bookings list or confirmation page
            }

            // Debugging output for ModelState errors
            foreach (var state in ModelState)
            {
                Console.WriteLine($"Key: {state.Key}, Value: {state.Value}");
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }

            // If we reach this point, something went wrong
            return View("BookingDetails", model);
        }










    }
}
