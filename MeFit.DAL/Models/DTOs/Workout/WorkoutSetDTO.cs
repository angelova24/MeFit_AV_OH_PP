using MeFit.DAL.Models.DTOs.Set;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.DTOs.Workout
{
    public class WorkoutSetDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(70)]
        public string Name { get; set; }
        public string Type { get; set; }
        public List<SetReadDTO> Sets { get; set; }
    }
}
