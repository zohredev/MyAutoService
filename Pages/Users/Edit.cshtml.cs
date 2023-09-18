using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Utilities;
using System.Data;

namespace MyAutoService.Pages.Users
{
    [Authorize(Roles = StaticData.AdminEndUser)]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }
        [BindProperty]
        public string SelectedRole { get; set; }
        public SelectList Roles { get; set; }
        public EditModel(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> OnGet(string id)
        {
            if (id.Trim().Length == 0)
                return NotFound();
            ApplicationUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (ApplicationUser == null)
                return NotFound();
            var userRoles = _userManager.GetRolesAsync(new IdentityUser() { Id = id }).Result;
            Roles = new SelectList(_roleManager.Roles, "Name", "Name", userRoles.First());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            var userInDb = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == ApplicationUser.Id);
            if (userInDb == null) return NotFound();

            userInDb.Name = ApplicationUser.Name;
            userInDb.Address = ApplicationUser.Address;
            userInDb.PhoneNumber = ApplicationUser.PhoneNumber;
            userInDb.PostalCode = ApplicationUser.PostalCode;
            userInDb.City = ApplicationUser.City;

            var userRoles = _userManager.GetRolesAsync(new IdentityUser() { Id = ApplicationUser.Id }).Result;
            if (SelectedRole != userRoles.FirstOrDefault())
            {
                await _userManager.RemoveFromRoleAsync(new IdentityUser() { Id = ApplicationUser.Id }, userRoles.First());
                await _userManager.AddToRoleAsync(new IdentityUser() { Id = ApplicationUser.Id }, SelectedRole);
            }


            _context.Update(userInDb);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");

        }
    }
}
