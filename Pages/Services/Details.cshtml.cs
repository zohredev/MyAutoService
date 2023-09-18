using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;

namespace MyAutoService.Pages.Services
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public ServiceHeader ServiceHeader { get; set; }
        [BindProperty]
        public List<ServiceDetails> ServiceDetails { get; set; }

        public IActionResult OnGet(int serviceid)
        {
            ServiceHeader = _context.ServiceHeaders.Include(u => u.Car)
                .Include(u => u.Car.ApplicationUser)
                .FirstOrDefault(u => u.Id == serviceid);

            if (ServiceDetails == null)
            {
                return NotFound();
            }
            ServiceDetails=_context.ServiceDetails.Where(u=>u.ServiceHeaderId==serviceid).ToList() ;
            return Page();
        }
    }
}
