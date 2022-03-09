using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.DTOs.UserDTOs
{
    public class UserCreateDTO
    {
       
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(30)]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }

        //WIP needs to be commented with team

        //public bool IsContributor { get; set; }
        //public bool IsAdmin { get; set; }
    }
}
