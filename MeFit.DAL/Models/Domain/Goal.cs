using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.Domain
{
    public class Goal
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool Achieved { get; set; }
        //FK and navigation properties
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<GoalWorkout> Workouts { get; set; } = new HashSet<GoalWorkout>();
    }
}
