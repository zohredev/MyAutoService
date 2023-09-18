using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyAutoService.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام مشتری")]
        [MaxLength(200)]
        public string? Name { get; set; }

        [Display(Name = "آدرس")]
        public string? Address { get; set; }


        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "شهر")]
        [MaxLength(200)]
        public string? City { get; set; }

        [Display(Name = "کد پستی")]
        [MaxLength(200)]
        public string? PostalCode { get; set; }
        [Display(Name = "ایمیل ")]
        public override string? Email { get => base.Email; set => base.Email = value; }
        [Display(Name = "تلفن")]
        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        public virtual List<Car> Cars { get; set; }
    }
}
