using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MeFit.DAL.Models.DTOs.Profile
{
    public class ProfileReadDTO
    {
        public int Id { get; set; }

        public double Weight { get; set; }
        
        public double Height { get; set; }
        [MaxLength(100)]
        public string MedicalConditions { get; set; }
        [MaxLength(100)]
        public string Disabilities { get; set; }

        public List<int> Goals { get; set; }
    }
}
