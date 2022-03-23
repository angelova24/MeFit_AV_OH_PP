using System.ComponentModel.DataAnnotations;


namespace MeFit.DAL.Models.DTOs.Profile
{
    public class ProfileCreateDTO
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        [MaxLength(100)]
        public string MedicalConditions { get; set; }
        [MaxLength(100)]
        public string Disabilities { get; set; }
        [Range(1, 6, ErrorMessage = "Please enter level between {1} and {2}")]
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
    }
}
