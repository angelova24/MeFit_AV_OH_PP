namespace MeFitAPI.DAL.Models.Domain
{
    public class Profile
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public int  Height { get; set; }
        public string MedicalConditions { get; set; }
        public string Disabilities { get; set; }
    }
}
