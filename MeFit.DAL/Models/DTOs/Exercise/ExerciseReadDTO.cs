using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeFit.DAL.Models.DTOs.Exercise
{
    public class ExerciseReadDTO
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
    }
}
