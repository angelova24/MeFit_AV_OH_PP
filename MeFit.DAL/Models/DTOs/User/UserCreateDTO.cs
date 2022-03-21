using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.DTOs.User
{
    public class UserCreateDTO
    {

        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(80)]
        public string Name { get; set; }
    }
}
