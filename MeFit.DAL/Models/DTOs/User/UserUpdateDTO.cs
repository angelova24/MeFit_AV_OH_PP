using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.DTOs.User
{
    public class UserUpdateDTO
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(80)]
        public string Name { get; set; }
        //WIP needs to be commented with team

        //public bool IsContributor { get; set; }
        //public bool IsAdmin { get; set; }
    }
}
