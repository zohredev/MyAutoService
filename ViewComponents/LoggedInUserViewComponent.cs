using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Models.ViewModels;

namespace MyAutoService.ViewComponents
{
    public class LoggedInUserViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public LoggedInUserViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _context.ApplicationUsers.FirstAsync(u => u.Email == User.Identity.Name);
            LoggedInUserViewModel viewModel = new LoggedInUserViewModel()
            {
               Name = user.Name,
                ShoppingCart = await _context.ServicesShoppingCarts
                    .Include(c => c.Car).ThenInclude(c => c.ApplicationUser)
                    .Include(c => c.ServiceType)
                    .Where(u => u.Car.ApplicationUser.Email == User.Identity.Name)
                    .ToListAsync()
            };
            return View("~/Pages/Shared/Components/LoggedInUser.cshtml",viewModel);
        }

    }
}
