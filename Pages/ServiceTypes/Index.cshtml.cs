using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Utilities;

namespace MyAutoService.Pages.ServiceTypes
{
    [Authorize(Roles=StaticData.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<ServiceType> ServiceTypes { get; set; }
        public async Task<IActionResult> OnGet()
        {
            ServiceTypes = await _context.ServiceTypes.ToListAsync();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            var service = _context.ServiceTypes.Find(id);
            if (service != null)
            {
                _context.ServiceTypes.Remove(service);
                return Page();
            }
            return NotFound();
        }
    }
}
