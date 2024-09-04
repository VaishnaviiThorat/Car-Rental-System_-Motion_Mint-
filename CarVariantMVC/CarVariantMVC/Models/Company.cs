using System;
using System.Collections.Generic;

namespace CarVariantMVC.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Variant> Variants { get; set; } = new List<Variant>();
}
