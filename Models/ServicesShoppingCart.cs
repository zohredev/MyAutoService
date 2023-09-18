using NuGet.Protocol;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAutoService.Models
{
    public class ServicesShoppingCart:BaseModel
    {
        public int CarId  { get; set; }
        public int ServiceTypeId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }
    }
}
