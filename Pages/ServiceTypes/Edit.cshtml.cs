using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Utilities;
using System.Data;

namespace MyAutoService.Pages.ServiceTypes
{
    [Authorize(Roles = StaticData.AdminEndUser)]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ServiceType ServiceType { get; set; }
        public IActionResult OnGet(int id)
        {
            ServiceType = _context.ServiceTypes.Find(id);
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();


            _context.ServiceTypes.Update(ServiceType);
            _context.SaveChanges();
            return RedirectToPage("Index");

        }
    }
}
