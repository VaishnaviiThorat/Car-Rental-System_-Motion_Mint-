using System;
using System.Collections.Generic;

namespace CarVariantMVC.Models;

public partial class Variant
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? FuelType { get; set; }

    public string? Image { get; set; }

    public bool Isac { get; set; }

    public string? ModelNumber { get; set; }

    public string? Name { get; set; }

    public decimal? PricePerDay { get; set; }

    public int SeatingCapacity { get; set; }

    public string? Status { get; set; }

    public int Year { get; set; }

    public int? CompanyId { get; set; }

    public string? City { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Company? Company { get; set; }
}
