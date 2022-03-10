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


        //navigation properties
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Goal> Goals { get; set; } = new HashSet<Goal>();
    }
}
