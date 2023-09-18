using System.ComponentModel.DataAnnotations.Schema;

namespace MyAutoService.Models
{
    public class ServiceDetails:BaseModel
    {
        public int ServiceHeaderId { get; set; }
        public int ServiceTypeId { get; set; }
        public double ServicePrice { get; set; }
        public string? ServiceName { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }
        [ForeignKey("ServiceHeaderId")]
        public virtual ServiceHeader  ServiceHeader { get; set;}
    }
}
