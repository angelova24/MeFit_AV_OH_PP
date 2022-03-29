using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.DTOs.Workout
{
    public class WorkoutUpdateDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(70)]
        public string Name { get; set; }
        public string Type { get; set; }
        public int OwnerId { get; set; }
    }
}
