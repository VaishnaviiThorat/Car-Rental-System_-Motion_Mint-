using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarVariantMVC.Models
{
    public class FeedbackViewModel
    {
        public int BookingId { get; set; }
        public int Rating { get; set; } // Rating from 1 to 5
        public string Comments { get; set; }
        public List<SelectListItem> Bookings { get; set; }= new List<SelectListItem>();
    }

}
