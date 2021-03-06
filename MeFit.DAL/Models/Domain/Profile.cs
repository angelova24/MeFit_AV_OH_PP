using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.Domain
{
    public class Profile
    {
        public int Id { get; set; }
        public double Weight { get; set; }    
        public double Height { get; set; }
        [MaxLength(100)]        
        public string MedicalConditions { get; set; }
        [MaxLength(100)]
        public string Disabilities { get; set; }
        [Range(1,6, ErrorMessage ="Please enter level between {1} and {2}")]
        public int FintessLevelEvaluation { get; set; }
        [MaxLength(150)]
        public string AddressLine1 { get; set; }
        [MaxLength(150)]
        public string AddressLine2 { get; set; }
        [MaxLength(15)]
        public string PostalCode { get; set; }
        [MaxLength(70)]
        public string City { get; set; }
        [MaxLength(70)]
        public string Country { get; set; }

        //navigation properties
        //public int? UserId { get; set; }
        public User User { get; set; }
        public ICollection<Goal> Goals { get; set; } = new HashSet<Goal>();
    }
}
