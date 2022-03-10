using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeFit.DAL.Models.DTOs.Workout
{
    public class WorkoutReadDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(70)]
        public string Name { get; set; }
        public string Type { get; set; }

        public List<int> Sets { get; set; }
        public List<int> Programs { get; set; }
        public List<int> Goals { get; set; }
    }
}
