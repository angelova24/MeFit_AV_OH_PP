using System;
using System.ComponentModel.DataAnnotations;


namespace MeFit.DAL.Models.DTOs.Goal
{
    public class GoalCreateDTO
    {

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        

    }
}
