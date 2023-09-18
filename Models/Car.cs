using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAutoService.Models
{
    public class Car:BaseModel
    {
        [Display(Name = "نام ماشین")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string? Name { get; set; }
        [Display(Name = "مدل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string? Model { get; set; }
        [Display(Name = "رنگ")]
        [MaxLength(50)]
        public string? Color { get; set; }
        [Display(Name = "سال")]
        [MaxLength(10)]
        public string? Year { get; set; }
        [Display(Name = "تصویر")]
        [MaxLength(50)]
        public string? ImageName  { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public List<ServicesShoppingCart> ServicesShoppingCarts { get; set; }
    }
}
