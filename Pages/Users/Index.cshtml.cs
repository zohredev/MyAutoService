using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Models.ViewModels;
using MyAutoService.Utilities;
using System.Data;
using System.Text;

namespace MyAutoService.Pages.User
{
    [Authorize(Roles = StaticData.AdminEndUser)]
    public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		[BindProperty]
		public UsersListViewModel UsersListView { get; set; }
		public IndexModel(ApplicationDbContext context)
		{
			_context = context;

		}
		public async Task<IActionResult> OnGet(int pageId = 1, string searchName = null, string searchEmail = null, string searchPhone = null)
		{
			UsersListView = new UsersListViewModel()
			{
				Users = await _context.ApplicationUsers.ToListAsync()

			};
			StringBuilder sb = new StringBuilder();
			sb.Append("/Users?pageId=:");
			var query = _context.ApplicationUsers.AsQueryable();
			if (searchName != null)
			{
				sb.Append("&searchName=");
				sb.Append(searchName);
				query = query.Where(u => u.Name.Contains(searchName));
			}
			if (searchEmail != null)
			{
				sb.Append("&searchEmail=");
				sb.Append(searchEmail);
				query = query.Where(u => u.Email.Contains(searchEmail));
			}
			if (searchPhone != null)
			{
				sb.Append("&searchPhone=");
				sb.Append(searchPhone);
				query = query.Where(u => u.PhoneNumber.Contains(searchPhone));
			}
			if (searchPhone != null || (searchEmail != null) || (searchName != null))
				UsersListView.Users = query.ToList();

			var count = UsersListView.Users.Count;
			UsersListView.PagingInfo = new PagingInfo()
			{
				CurrentPage = pageId,
				ItemPerPage = StaticData.PagingUserCount,
				TotalItems = count,
				UrlParams = sb.ToString()
			};

			//skip mipare va take miare
			UsersListView.Users = UsersListView.Users.OrderBy(u => u.Name)
				.Skip((pageId - 1) * StaticData.PagingUserCount)
				.Take(StaticData.PagingUserCount)
				.ToList();
			return Page();
		}
	}
}
