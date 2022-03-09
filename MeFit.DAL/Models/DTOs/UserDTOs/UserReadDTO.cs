using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.DTOs.UserDTOs
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(30)]
       
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
    }
}