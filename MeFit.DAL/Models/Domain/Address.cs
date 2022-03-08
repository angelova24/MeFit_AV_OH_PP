using System.ComponentModel.DataAnnotations;

namespace MeFitAPI.DAL.Models.Domain
{
    public class Address
    {
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string AddressLine1 { get; set; }
        [MaxLength(150)]
        public string AddressLine2 { get; set; }
        [Required, MaxLength(15)]
        public string PostalCode { get; set; }
        [Required, MaxLength(70)]
        public string City { get; set; }
        [Required, MaxLength(70)]
        public string Country { get; set; }
        //FK and navigation property
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

    }
}
