using System;
using System.ComponentModel.DataAnnotations;


namespace MeFit.DAL.Models.DTOs.GoalsDTO
{
    public class GoalUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool Achieved { get; set; }
       
    }
}
