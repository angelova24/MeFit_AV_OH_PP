using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.Domain
{
    public class Set
    {
        public int Id { get; set; }
        public int ExerciseRepetitions { get; set; }
        public int OwnerId { get; set; }

        //FK with navigation properties
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public ICollection<Workout> Workouts { get; set; } = new HashSet<Workout>();
    }
}
