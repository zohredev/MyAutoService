using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAutoService.Data;
using MyAutoService.Models;

namespace MyAutoService.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }
       
        [BindProperty]
        public List<ServiceType> ServiceTypes { get; set; }
        public List<Car> Cars { get; set; }
        public void OnGet()
        {
            ServiceTypes = _context.ServiceTypes.ToList();
            Cars = _context.Cars.ToList();
        }
    }
}