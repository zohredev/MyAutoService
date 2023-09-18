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
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public CreateModel(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public ServiceType ServiceType { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(ServiceType serviceType)
        {
            if(ModelState.IsValid)
            {
                _dbContext.ServiceTypes.Add(serviceType);
                _dbContext.SaveChanges();
                return RedirectToPage("Index"); 
            }
            return Page();
        }
    }
}
