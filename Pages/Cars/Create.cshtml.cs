using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Models.ViewModels;
using System.Security.Claims;

namespace MyAutoService.Pages.Cars
{
	public class CreateModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		[BindProperty]
		public Car Car { get; set; }
		[BindProperty]
		public IFormFile ImgUp { get; set; }
		public CreateModel(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult OnGet(string userId = null)
		{
			Car = new Car();
			if (userId == null)
			{
				var claimsIdentity = (ClaimsIdentity)User.Identity;
				var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
				userId = claim.Value;
			}
			Car.UserId = userId;
			return Page();
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			if (ImgUp != null)
			{
				Car.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
				string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", Car.ImageName);
				using (var fileStream = new FileStream(savePath, FileMode.Create))
				{
					ImgUp.CopyTo(fileStream);
				}

			}
			_context.Cars.Add(Car);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
