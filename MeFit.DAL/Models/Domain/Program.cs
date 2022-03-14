using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.Domain
{
    public class Program
    {
        public int Id { get; set; }
        [Required, MaxLength(70)]
        public string Name { get; set; }
        [Required, MaxLength(70)]
        public string Category { get; set; }

        public int OwnerId { get; set; }
        //navigation property
        public ICollection<Workout> Workouts { get; set; } = new HashSet<Workout>();
    }
}
