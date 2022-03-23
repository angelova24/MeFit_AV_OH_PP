using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeFit.DAL.Models.DTOs.Goal
{
    public class GoalWorkoutReadDTO
    {
        public int Id { get; set; }
        public bool Complete { get; set; }
        public int GoalId { get; set; }
        public int WorkoutId { get; set; }
    }
}
