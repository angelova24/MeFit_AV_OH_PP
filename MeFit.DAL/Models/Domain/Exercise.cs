
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.Domain
{
    public class Exercise
    {
        public int Id { get; set; }
        [Required, MaxLength(70)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required, MaxLength(70)]
        public string TargetMuscleGroup { get; set; }
        [MaxLength(200)]
        public string ImageURL { get; set; }
        [MaxLength(200)]
        public string VideoURL { get; set; }
        //navigation property
        public ICollection<Set> Sets { get; set; } = new HashSet<Set>();

    }
}
