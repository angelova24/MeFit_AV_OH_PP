using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.DTOs.User
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public int ProfileId { get; set; }
    }
}