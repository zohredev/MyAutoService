using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAutoService.Models
{
    public class ServiceHeader : BaseModel
    {
        public double? Miles { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public string? Description { get; set; }
        [Required]
        public int CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
    }
}
