using System.ComponentModel.DataAnnotations;

namespace MyAutoService.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "yyyy/MM/dd")]
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }

    }
}
