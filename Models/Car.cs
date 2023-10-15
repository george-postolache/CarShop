using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Required, StringLength(17, MinimumLength = 17)]
        public string VIN { get; set; }
        [Required, MaxLength(50)]
        public string Make { get; set; }
        [Required, MaxLength(50)]
        public string Model { get; set; }
        [MaxLength(50)]
        public string Color { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }

        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
