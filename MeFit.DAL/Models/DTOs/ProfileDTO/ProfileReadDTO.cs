using System.ComponentModel.DataAnnotations;


namespace MeFit.DAL.Models.DTOs.ProfileDTO
{
    public class ProfileReadDTO
    {
        
        [MaxLength(10)]
        public double Weight { get; set; }
        [MaxLength(10)]
        public double Height { get; set; }
        [MaxLength(100)]
        public string MedicalConditions { get; set; }
        [MaxLength(100)]
        public string Disabilities { get; set; }
    }
}
