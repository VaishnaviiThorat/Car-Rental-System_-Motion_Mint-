//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Linq;
//using System.Net;
//using System.Net.Mail;
//using System.Collections.Generic;
//using CarVariantMVC.Models; // Adjust namespace as per your project
//using System.Threading.Tasks;

//namespace CarVariantMVC.Controllers
//{
//    public class FeedbackController : Controller
//    {
//        private readonly CarVariantTestContext _context;

//        public FeedbackController(CarVariantTestContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Create()
//        {
//            var userId = GetUserIdFromCookie();
//            var bookings = _context.Bookings
//                .Where(b => b.UserId == userId)
//                .Select(b => new SelectListItem
//                {
//                    Value = b.Id.ToString(),
//                    Text = $"{b.Variant.Name} - {b.BookingDate.ToShortDateString()}"
//                }).ToList();

//            var model = new FeedbackViewModel
//            {
//                Bookings = bookings
//            };

//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(FeedbackViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Get the email of the logged-in user
//                var userEmail = GetUserEmailById(GetUserIdFromCookie());

//                // Construct the email message
//                string emailTo = "parasseth16121999@gmail.com";
//                string subject = $"Feedback for Booking ID: {model.BookingId}";
//                string body = $"Rating: {model.Rating}\n\nComments: {model.Comments}";

//                using (var smtpClient = new SmtpClient("smtp.gmail.com"))
//                {
//                    smtpClient.UseDefaultCredentials = false;
//                    smtpClient.Credentials = new NetworkCredential("parasseth16121999@gmail.com", "Computer@96");
//                    smtpClient.EnableSsl = true;
//                    smtpClient.Port = 587; // Usually the port is 587 for SSL

//                    await smtpClient.SendMailAsync(new MailMessage(userEmail, emailTo, subject, body));
//                }

//                // Show success message after sending feedback
//                ViewBag.Success = true;
//                return View(model);
//            }

//            return View(model);
//        }

//        private int GetUserIdFromCookie()
//        {
//            var userIdCookie = Request.Cookies["UserId"];
//            return int.Parse(userIdCookie);
//        }

//        private string GetUserEmailById(int userId)
//        {
//            var user = _context.Users.Find(userId);
//            return user?.Email;
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using CarVariantMVC.Models; // Adjust namespace as per your project
using System.Threading.Tasks;

namespace CarVariantMVC.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly CarVariantTestContext _context;

        public FeedbackController(CarVariantTestContext context)
        {
            _context = context;
        }

        //// GET: Feedback/Create
        //public IActionResult Create()
        //{
        //    var userId = GetUserIdFromCookie();
        //    if (userId == 0)
        //    {
        //        return RedirectToAction("Login", "Account"); // Redirect to login if user is not logged in
        //    }

        //    var bookings = _context.Bookings
        //        .Where(b => b.UserId == userId)
        //        .Select(b => new SelectListItem
        //        {
        //            Value = b.Id.ToString(),
        //            Text = $"{b.Variant.Name} - {b.BookingDate.ToShortDateString()}"
        //        }).ToList();

        //    var model = new FeedbackViewModel
        //    {
        //        Bookings = bookings
        //    };

        //    return View(model);
        //}
        // GET: Feedback/Create
        public IActionResult Create()
        {
            // Retrieve userId from the cookie
            var userId = GetUserIdFromCookie();

            // Check if user is not logged in or userId is invalid
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not logged in
            }

            // Fetch bookings related to the logged-in user
            var bookings = _context.Bookings
                .Where(b => b.UserId == userId)
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = $"{b.Variant.Name} - {b.BookingDate.ToShortDateString()}"
                }).ToList();

            // Create a new FeedbackViewModel and populate the Bookings list
            var model = new FeedbackViewModel
            {
                Bookings = bookings
            };

            // Return the view with the populated model
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeedbackViewModel model)
        {
            Console.WriteLine("Inside Create POST");

            if (ModelState.IsValid)
            {
                Console.WriteLine("Model is valid");

                var userEmail = GetUserEmailById(GetUserIdFromCookie());
                if (userEmail == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to find your email address.");
                    ReloadBookings(model); // Reload bookings before returning the view
                    return View(model);
                }
                Console.WriteLine(userEmail);

                // Construct the email message
                string emailTo = "parasseth16121999@gmail.com"; // Your admin email
                string subject = $"Feedback for Booking ID: {model.BookingId}";
                string body = $"From: {userEmail}\nRating: {model.Rating}\n\nComments: {model.Comments}";

                try
                {
                    using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential("parasseth16121999@gmail.com", "tfwdakriwrdqbqsh"); // Admin email credentials
                        smtpClient.EnableSsl = true;
                        smtpClient.Port = 587;

                        // Create the MailMessage
                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress(userEmail), // Admin email as the sender
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = false // Set to true if you want to send HTML content
                        };

                        mailMessage.To.Add(emailTo); // Add admin email as the actual recipient
                                                     // Optionally add userEmail to BCC if you want the user to also get a copy
                                                     // mailMessage.Bcc.Add(userEmail); 

                        await smtpClient.SendMailAsync(mailMessage);
                    }

                    Console.WriteLine("Email sent successfully");
                    ViewBag.IsSuccess = true; // Indicate success
                    return View(model);
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    ModelState.AddModelError(string.Empty, $"Error sending email: {ex.Message}");
                }
            }

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }

            ReloadBookings(model); // Reload bookings before returning the view

            return View(model);
        }


        // Helper method to reload bookings
        private void ReloadBookings(FeedbackViewModel model)
        {
            var userId = GetUserIdFromCookie();
            model.Bookings = _context.Bookings
                .Where(b => b.UserId == userId)
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = $"{b.Variant.Name} - {b.BookingDate.ToShortDateString()}"
                }).ToList();
        }



        //// POST: Feedback/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(FeedbackViewModel model)
        //{
        //    Console.WriteLine("inside create post");
        //    if (ModelState.IsValid)
        //    {
        //        Console.WriteLine("valid model");
        //        var userEmail = GetUserEmailById(GetUserIdFromCookie());
        //        if (userEmail == null)
        //        {
        //            ModelState.AddModelError(string.Empty, "Unable to find your email address.");
        //            return View(model);
        //        }

        //        // Construct the email message
        //        string emailTo = "parasseth16121999@gmail.com"; // Your admin email
        //        string subject = $"Feedback for Booking ID: {model.BookingId}";
        //        string body = $"Rating: {model.Rating}\n\nComments: {model.Comments}";

        //        try
        //        {
        //            using (var smtpClient = new SmtpClient("smtp.gmail.com"))
        //            {
        //                smtpClient.UseDefaultCredentials = false;
        //                Console.WriteLine("before mail");
        //                smtpClient.Credentials = new NetworkCredential("parasseth16121999@gmail.com", "tfwdakriwrdqbqsh"); // Admin email credentials
        //                Console.WriteLine("After mail");
        //                smtpClient.EnableSsl = true;
        //                smtpClient.Port = 587;

        //                await smtpClient.SendMailAsync(new MailMessage(userEmail, emailTo, subject, body));
        //            }

        //            ViewBag.IsSuccess = true; // Indicate success
        //            return View(model);
        //        }
        //        catch (SmtpException ex)
        //        {
        //            ModelState.AddModelError(string.Empty, $"Error sending email: {ex.Message}");
        //        }
        //    }

        //    foreach (var modelState in ModelState.Values)
        //    {
        //        foreach (var error in modelState.Errors)
        //        {
        //            Console.WriteLine(error.ErrorMessage);
        //        }
        //    }


        //    // Reload the bookings if the model state is invalid
        //    model.Bookings = _context.Bookings
        //        .Where(b => b.UserId == GetUserIdFromCookie())
        //        .Select(b => new SelectListItem
        //        {
        //            Value = b.Id.ToString(),
        //            Text = $"{b.Variant.Name} - {b.BookingDate.ToShortDateString()}"
        //        }).ToList();

        //    return View(model);
        //}

        private int GetUserIdFromCookie()
        {
            var userIdCookie = Request.Cookies["UserId"];
            return int.TryParse(userIdCookie, out int userId) ? userId : 0;
        }

        private string GetUserEmailById(int userId)
        {
            var user = _context.Users.Find(userId);
            return user?.Email;
        }
    }
}

