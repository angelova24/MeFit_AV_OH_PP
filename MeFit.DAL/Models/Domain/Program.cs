using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFitAPI.DAL.Models.Domain
{
    public class Program
    {
        public int Id { get; set; }
        [Required, MaxLength(70)]
        public string Name { get; set; }
        [Required, MaxLength(70)]
        public string Category { get; set; }
        //navigation property
        public ICollection<Workout> Workouts { get; set; } = new HashSet<Workout>();
    }
}
