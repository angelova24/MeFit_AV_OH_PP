namespace MeFitAPI.DAL.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsContributor { get; set; }
        public bool isAdmin { get; set; }
    }
}
