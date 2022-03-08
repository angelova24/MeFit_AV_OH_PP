using System.ComponentModel.DataAnnotations;

namespace MeFitAPI.DAL.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(30)]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public bool IsContributor { get; set; }
        public bool IsAdmin { get; set; }
        //FK and navigation property
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
