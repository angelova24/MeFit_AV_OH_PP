using MeFit.DAL.Models.DTOs.Workout;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeFit.DAL.Models.DTOs.Goal
{
    public class GoalWorkoutsDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool Achieved { get; set; }

        public List<WorkoutCompleteDTO> Workouts { get; set; }
    }
}
