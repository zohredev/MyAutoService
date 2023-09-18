using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Utilities;
using System.Data;

namespace MyAutoService.Pages.Users
{
    [Authorize(Roles = StaticData.AdminEndUser)]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DeleteModel(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id.Trim().Length == 0)
                return NotFound();

            ApplicationUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);

            if (ApplicationUser == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id.Trim().Length == 0)
                return NotFound();

            ApplicationUser = await _context.ApplicationUsers.FindAsync(id);
            if (ApplicationUser == null)
            {
                return NotFound();
            }
            _context.ApplicationUsers.Remove(ApplicationUser);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
