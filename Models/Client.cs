using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Required, StringLength(13, MinimumLength = 13)]
        public string PIN { get; set; }
        [Required, MaxLength(150)]
        public string FirstName { get; set; }
        [Required, MaxLength(150)]
        public string LastName { get; set; }
        [Required, MaxLength(10)]
        public string PhoneNumber { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
