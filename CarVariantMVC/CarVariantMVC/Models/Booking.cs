using System;
using System.Collections.Generic;

namespace CarVariantMVC.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int VariantId { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User User { get; set; } = null!;

    public virtual Variant Variant { get; set; } = null!;
}
