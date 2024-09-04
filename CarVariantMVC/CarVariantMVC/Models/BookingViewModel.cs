namespace CarVariantMVC.Models
{
    public class BookingViewModel
    {
        public int VariantId { get; set; }
        public string VariantName { get; set; }
        public string CompanyName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal PricePerDay { get; set; }
    }

}
