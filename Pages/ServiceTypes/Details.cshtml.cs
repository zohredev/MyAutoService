using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Models;
using MyAutoService.Utilities;
using System.Data;

namespace MyAutoService.Pages.ServiceTypes
{
    [Authorize(Roles = StaticData.AdminEndUser)]
    public class DetailsModel : PageModel
    {
        private readonly MyAutoService.Data.ApplicationDbContext _context;

        public DetailsModel(MyAutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public ServiceType ServiceType { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ServiceTypes == null)
            {
                return NotFound();
            }

            var servicetype = await _context.ServiceTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (servicetype == null)
            {
                return NotFound();
            }
            else 
            {
                ServiceType = servicetype;
            }
            return Page();
        }
    }
}
