using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFitAPI.DAL.Models.Domain
{
    public class Workout
    {
        public int Id { get; set; }
        [Required, MaxLength(70)]
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Complete { get; set; }
        //navigation properties
        public ICollection<Set> Sets { get; set; } = new HashSet<Set>();
        public ICollection<Program> Programs { get; set; } = new HashSet<Program>();
        public ICollection<GoalWorkout> Goals { get; set; } = new HashSet<GoalWorkout>();
    }
}
