using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Models.ViewModels;
using System.Security.Claims;

namespace MyAutoService.Pages.Cars
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		[BindProperty]
		public CarAndCustomerViewModel CarAndCustomerViewModel { get; set; }
		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> OnGet(string userId = null)
		{
			if (userId == null)
			{
				var claimsIdentity = (ClaimsIdentity)User.Identity;
				var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
				userId = claim.Value;
			}
			CarAndCustomerViewModel = new CarAndCustomerViewModel()
			{
				Cars = await _context.Cars.Where(u => u.UserId == userId).ToListAsync(),
				User = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId)
			};
			
			return Page();
		}
		public void OnPost()
		{

		}
	}
}
