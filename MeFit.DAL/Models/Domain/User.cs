using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(80)]
        public string Name { get; set; }

        //FK and navigation property
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
