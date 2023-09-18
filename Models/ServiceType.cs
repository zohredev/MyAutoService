using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyAutoService.Models
{
    public class ServiceType:BaseModel
    {
        [DisplayName("نام")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [DisplayName("قیمت")]
        [Required]
        public double Price { get; set; }

    }
}
