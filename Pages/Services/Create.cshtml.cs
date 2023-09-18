using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Models.ViewModels;

namespace MyAutoService.Pages.Services
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public CarServiceViewModel CarServiceViewModel { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int? carId)
        {
            CarServiceViewModel = new CarServiceViewModel()
            {
                //include hamoon with hast ke tuye laravel baraye avordane relation ha estefade mishe
                Car = await _context.Cars.Include(u => u.ApplicationUser)
                .FirstOrDefaultAsync(u => u.Id == carId),
                ServiceHeader = new ServiceHeader()
            };

            List<string> listServiceTypeInShoppingCart = await _context.ServicesShoppingCarts.Include(u => u.ServiceType)
                .Where(u => u.CarId == carId)
                .Select(u => u.ServiceType.Name).ToListAsync();

            IQueryable<ServiceType> serviceTypes = from s in _context.ServiceTypes
                                                   where !(listServiceTypeInShoppingCart.Contains(s.Name))
                                                   select s;
            CarServiceViewModel.ServiceTypes = serviceTypes.ToList();
            CarServiceViewModel.ServicesShoppingCarts = await _context.ServicesShoppingCarts.Include(u => u.ServiceType)
                .Where(u => u.CarId == carId).ToListAsync();
            CarServiceViewModel.ServiceHeader.TotalPrice = 0;
            foreach (var item in CarServiceViewModel.ServicesShoppingCarts)
            {
                CarServiceViewModel.ServiceHeader.TotalPrice += item.ServiceType.Price;
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                CarServiceViewModel.ServicesShoppingCarts = await _context.ServicesShoppingCarts
                    .Include(c => c.ServiceTypeId)
                    .Where(c => c.CarId == CarServiceViewModel.Car.Id).ToListAsync();
                foreach (var shop in CarServiceViewModel.ServicesShoppingCarts)
                {
                    CarServiceViewModel.ServiceHeader.TotalPrice += shop.ServiceType.Price;
                }
                CarServiceViewModel.ServiceHeader.CarId = CarServiceViewModel.Car.Id;
                _context.ServiceHeaders.Add(CarServiceViewModel.ServiceHeader);
                await _context.SaveChangesAsync();

                foreach (var shop in CarServiceViewModel.ServicesShoppingCarts)
                {
                    ServiceDetails serviceDetails = new ServiceDetails()
                    {
                        ServiceHeaderId = CarServiceViewModel.ServiceHeader.Id,
                        ServiceName = shop.ServiceType.Name,
                        ServiceTypeId = shop.ServiceTypeId,
                        ServicePrice = shop.ServiceType.Price,
                    };

                    _context.Add(serviceDetails);
                }
                _context.ServicesShoppingCarts.RemoveRange(CarServiceViewModel.ServicesShoppingCarts);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Cars/Index", new { userId = CarServiceViewModel.Car.UserId });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCart()
        {
            ServicesShoppingCart shoppingCart = new ServicesShoppingCart()
            {
                CarId = CarServiceViewModel.Car.Id,
                ServiceTypeId = CarServiceViewModel.ServiceDetails.ServiceTypeId
            };
            _context.ServicesShoppingCarts.Add(shoppingCart);
            await _context.SaveChangesAsync();
            return RedirectToPage("Create", new { carId = CarServiceViewModel.Car.Id });
        }

        public async Task<IActionResult> OnPostRemoveFromCart(int serviceTypeId)
        {
            ServicesShoppingCart shoppingCart = await _context.ServicesShoppingCarts
                .FirstAsync(u => u.CarId == CarServiceViewModel.Car.Id && u.ServiceTypeId == serviceTypeId);
            if (shoppingCart != null)
            {
                _context.Remove(shoppingCart);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Create", new { carId = CarServiceViewModel.Car.Id });
        }

    }
}
