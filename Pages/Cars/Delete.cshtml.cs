using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;

namespace MyAutoService.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        private readonly MyAutoService.Data.ApplicationDbContext _context;

        public DeleteModel(MyAutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Car Car { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? carId)
        {
            if (carId == null || _context.Cars == null)
            {
                return NotFound();
            }

            Car = await _context.Cars.FirstOrDefaultAsync(m => m.Id == carId);

            if (Car == null)
            {
                return NotFound();
            } 
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? carId)
        {
            if (carId == null || _context.Cars == null)
            {
                return NotFound();
            }
            var car = await _context.Cars.FindAsync(carId);

            if (car != null)
            {
                Car = car;
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", Car.ImageName);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                _context.Cars.Remove(Car);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
