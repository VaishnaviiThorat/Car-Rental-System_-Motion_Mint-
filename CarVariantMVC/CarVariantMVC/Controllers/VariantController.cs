using CarVariantMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarVariantMVC.Controllers;
public class VariantController : Controller
{
    private readonly CarVariantTestContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public VariantController(CarVariantTestContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }
    // POST: Variant/Edit/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id, VariantViewModel model)
    //{
    //    if (id != model.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        var variant = await _context.Variants.FindAsync(id);
    //        if (variant == null)
    //        {
    //            return NotFound();
    //        }

    //        // Update the variant properties
    //        variant.Description = model.Description;
    //        variant.FuelType = model.FuelType;
    //        variant.Isac = model.Isac;
    //        variant.ModelNumber = model.ModelNumber;
    //        variant.Name = model.Name;
    //        variant.PricePerDay = model.PricePerDay;
    //        variant.SeatingCapacity = model.SeatingCapacity;
    //        variant.Status = model.Status;
    //        variant.Year = model.Year;
    //        variant.CompanyId = model.CompanyId;
    //        variant.City = model.City;

    //        // Handle image update if necessary
    //        if (model.ImageFile != null)
    //        {
    //            // If a new image is uploaded, save it and update the variant's image path
    //            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
    //            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
    //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

    //            using (var fileStream = new FileStream(filePath, FileMode.Create))
    //            {
    //                await model.ImageFile.CopyToAsync(fileStream);
    //            }

    //            variant.Image = uniqueFileName; // Update image path
    //        }

    //        // Save changes to the database
    //        _context.Update(variant);
    //        await _context.SaveChangesAsync();

    //        return RedirectToAction("Index");
    //    }

    //    ViewBag.CompanyList = new SelectList(_context.Companies, "Id", "Name", model.CompanyId);
    //    return View(model);
    //}
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Edit(int id, Variant variant)
    //{
    //    if (id != variant.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(variant);
    //            _context.SaveChanges();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!VariantExists(variant.Id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }

    //    ViewBag.CompanyList = new SelectList(_context.Companies, "Id", "Name", variant.CompanyId);
    //    ViewBag.Cities = new List<SelectListItem>
    //{
    //    new SelectListItem { Value = "Mumbai", Text = "Mumbai" },
    //    new SelectListItem { Value = "Chennai", Text = "Chennai" },
    //    new SelectListItem { Value = "Pune", Text = "Pune" },
    //    new SelectListItem { Value = "Kanpur", Text = "Kanpur" }
    //};

    //    return View(variant);
    //}

    // Helper method to check if the variant exists

    private bool VariantExists(int id)
    {
        return _context.Variants.Any(e => e.Id == id);
    }

    // GET: Variant/Edit/5
    //[HttpGet]
    //public async Task<IActionResult> Edit(int id)
    //{
    //    var variant = await _context.Variants.FindAsync(id);
    //    if (variant == null)
    //    {
    //        return NotFound();
    //    }

    //    var model = new VariantViewModel
    //    {
    //        Id = variant.Id,
    //        Description = variant.Description,
    //        FuelType = variant.FuelType,
    //        Image = variant.Image, // This will be the string path to the image
    //        Isac = variant.Isac,
    //        ModelNumber = variant.ModelNumber,
    //        Name = variant.Name,
    //        PricePerDay = variant.PricePerDay.GetValueOrDefault(0),
    //        SeatingCapacity = variant.SeatingCapacity,
    //        Status = variant.Status,
    //        Year = variant.Year,
    //        CompanyId = variant.CompanyId.GetValueOrDefault(1)
    //    };

    //    ViewBag.CompanyList = new SelectList(_context.Companies, "Id", "Name", variant.CompanyId);
    //    return View(model);
    //}
    // GET: Variant/Edit/5

    //public IActionResult Index()
    //{
    //    var variants = _context.Variants
    //        .Include(v => v.Company) // Include the related Company
    //        .ToList();

    //    return View(variants);
    //}

    //[HttpGet]
    //public IActionResult Index()
    //{
    //    var variants = _context.Variants
    //  .Include(v => v.Company) // Include Company details
    //  .Select(v => new VariantViewModel
    //  { 

    //      Id = v.Id,
    //      Description = v.Description,
    //      ModelNumber = v.ModelNumber,
    //      FuelType = v.FuelType,
    //      Image = v.Image, // Assign the image file name here
    //      Name = v.Name,
    //      PricePerDay = v.PricePerDay.GetValueOrDefault(0),
    //      SeatingCapacity = v.SeatingCapacity,
    //      Status = v.Status,
    //      Year = v.Year,
    //      CompanyId = v.Id// Assuming Company is a navigational property
    //  })
    //  .ToList();


    //    return View(variants); // Return the list of VariantViewModel to the view
    //}



    // GET: Variant/Create
    //[HttpGet]
    //public IActionResult CreateVariant()
    //{
    //    ViewBag.CompanyList = new SelectList(_context.Companies, "Id", "Name");
    //    return View();
    //}
    //[HttpGet]
    //public IActionResult CreateVariant()
    //{
    //    ViewBag.CompanyList = new SelectList(_context.Companies, "Id", "Name");

    //    // Hardcoded list of cities
    //    var cities = new List<SelectListItem>
    //{
    //    new SelectListItem { Value = "Mumbai", Text = "Mumbai" },
    //    new SelectListItem { Value = "Chennai", Text = "Chennai" },
    //    new SelectListItem { Value = "Pune", Text = "Pune" },
    //    new SelectListItem { Value = "Kanpur", Text = "Kanpur" }
    //};
    //    ViewBag.CityList = cities; // Pass cities to the view

    //    return View();
    //}
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var variant = await _context.Variants.FindAsync(id);
        if (variant == null)
        {
            return NotFound();
        }

        ViewBag.CompanyList = new SelectList(_context.Companies, "Id", "Name", variant.CompanyId);

        // Create a VariantViewModel from the variant entity
        var viewModel = new VariantViewModel
        {
            Id = variant.Id,
            Description = variant.Description,
            FuelType = variant.FuelType,
            Image = variant.Image,
            Isac = variant.Isac,
            ModelNumber = variant.ModelNumber,
            Name = variant.Name,
            PricePerDay = variant.PricePerDay.GetValueOrDefault(0),
            SeatingCapacity = variant.SeatingCapacity,
            Status = variant.Status,
            Year = variant.Year,
            CompanyId = variant.CompanyId.GetValueOrDefault(0),
            City = variant.City
        };

        return View(viewModel);
    }
    // POST: Edit Variant
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(VariantViewModel model)
    {
        if (ModelState.IsValid)
        {
            var variant = await _context.Variants.FindAsync(model.Id);
            if (variant == null)
            {
                return NotFound();
            }

            // Update the variant properties
            variant.Description = model.Description;
            variant.FuelType = model.FuelType;
            variant.Isac = model.Isac;
            variant.ModelNumber = model.ModelNumber;
            variant.Name = model.Name;
            variant.PricePerDay = model.PricePerDay;
            variant.SeatingCapacity = model.SeatingCapacity;
            variant.Status = model.Status;
            variant.Year = model.Year;
            variant.CompanyId = model.CompanyId;
            variant.City = model.City;

            // Handle image upload
            if (model.ImageFile != null)
            {
                var imageName = Path.GetFileName(model.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                variant.Image = imageName; // Update image name in the variant
            }

            _context.Update(variant);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.CompanyList = new SelectList(_context.Companies, "Id", "Name", model.CompanyId);
        return View(model);
    }


public async Task<IActionResult> Index()
    {
        var variants = await _context.Variants
                                     .Select(v => new VariantViewModel
                                     {
                                         Id = v.Id,
                                         Name = v.Name,
                                         ModelNumber = v.ModelNumber,
                                         FuelType = v.FuelType,
                                         SeatingCapacity = v.SeatingCapacity,
                                         PricePerDay = v.PricePerDay ?? 0,
                                         Year = v.Year,
                                         City = v.City,
                                         CompanyId = v.CompanyId ?? 0,
                                         Status = v.Status,
                                         Isac = v.Isac,
                                         Image = v.Image,
                                         CompanyName=v.Company.Name
                                     })
                                     .ToListAsync();
       

        return View(variants);
    }

    public IActionResult CreateVariant()
    {
        ViewBag.CompanyList = new SelectList(_context.Companies, "Id", "Name");
        ViewBag.Cities = new SelectList(new List<string> { "Mumbai", "Pune", "Chennai", "Kanpur" });
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VariantViewModel model)
    {
        if (ModelState.IsValid)
        {
            string uniqueFileName = null;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }
            }

            var variant = new Variant
            {
                Name = model.Name,
                ModelNumber = model.ModelNumber,
                FuelType = model.FuelType,
                SeatingCapacity = model.SeatingCapacity,
                PricePerDay = model.PricePerDay,
                Year = model.Year,
                Isac = model.Isac,
                Status = model.Status,
                Description = model.Description,
                City = model.City, // Assign the selected city
                CompanyId = model.CompanyId,
                Image = uniqueFileName // Save the unique file name to the Image property
            };

            _context.Variants.Add(variant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.CompanyList = new SelectList(_context.Companies, "Id", "Name");
        ViewBag.Cities = new SelectList(new List<string> { "Mumbai", "Pune", "Chennai", "Kanpur" });
        return View(model);
    }



    // GET: Variant/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var variant = await _context.Variants
            .Include(v => v.Company)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (variant == null)
        {
            return NotFound();
        }

        // Map the Variant to VariantViewModel
        var model = new VariantViewModel
        {
            Id = variant.Id,
            Description = variant.Description,
            FuelType = variant.FuelType,
            Image = variant.Image,
            Isac = variant.Isac,
            ModelNumber = variant.ModelNumber,
            Name = variant.Name,
            PricePerDay = variant.PricePerDay.GetValueOrDefault(0),
            SeatingCapacity = variant.SeatingCapacity,
            Status = variant.Status,
            Year = variant.Year,
            CompanyId = variant.CompanyId.GetValueOrDefault(1)
        };

        return View(model);
    }
    

    // POST: Variant/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var variant = await _context.Variants.FindAsync(id);

        if (variant != null)
        {
            // Delete the associated image file if it exists
            if (!string.IsNullOrEmpty(variant.Image))
            {
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", variant.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Variants.Remove(variant);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }



    // GET: Variant/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var variant = await _context.Variants
            .Include(v => v.Company) // Include related Company details
            .FirstOrDefaultAsync(v => v.Id == id);

        if (variant == null)
        {
            return NotFound();
        }

        // Map the variant to VariantViewModel if necessary
        var model = new VariantViewModel
        {
            Id = variant.Id,
            Description = variant.Description,
            FuelType = variant.FuelType,
            Image = variant.Image,
            Isac = variant.Isac,
            ModelNumber = variant.ModelNumber,
            Name = variant.Name,
            PricePerDay = variant.PricePerDay.GetValueOrDefault(0),
            SeatingCapacity = variant.SeatingCapacity,
            Status = variant.Status,
            Year = variant.Year,
            CompanyId = variant.CompanyId.GetValueOrDefault(0),
            CompanyName = variant.Company.Name, // Assuming Company is a navigation property
            City = variant.City
        };

        return View(model);
    }

}


