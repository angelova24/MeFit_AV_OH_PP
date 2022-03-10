using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeFit.DAL.Models.DTOs.Workout
{
    public class WorkoutCreatDTO
    {
        [Required, MaxLength(70)]
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
