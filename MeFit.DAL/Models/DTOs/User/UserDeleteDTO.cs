using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.DTOs.User
{
    public class UserDeleteDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
    }
}
