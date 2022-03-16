using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MeFit.DAL.Models.DTOs.Goal
{
    public class GoalReadDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool Achieved { get; set; }

        public List<int> Workouts { get; set; }
    }
}
