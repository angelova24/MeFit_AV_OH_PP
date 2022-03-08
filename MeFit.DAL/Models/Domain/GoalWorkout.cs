using MeFitAPI.DAL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeFitAPI.DAL.Models.Domain
{
    public class GoalWorkout
    {
        public int Id { get; set; }
        public bool Complete { get; set; }

        //FK and navigation properties
        public int GoalId { get; set; }
        public Goal Goal { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
