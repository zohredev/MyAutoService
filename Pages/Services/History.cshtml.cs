using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;

namespace MyAutoService.Pages.Services
{
    [Authorize]
    public class HistoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public List<ServiceHeader> ServiceHeaders { get; set; }
        public string UserId { get; set; }
        public HistoryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(int carId)
        {
            ServiceHeaders = _context.ServiceHeaders.Include(u => u.Car)
                .Include(u => u.Car.ApplicationUser)
                .Where(u => u.CarId == carId)
                .ToList();

            UserId = _context.Cars.Where(u => u.Id == carId).FirstOrDefault().UserId;

        }
    }
}
