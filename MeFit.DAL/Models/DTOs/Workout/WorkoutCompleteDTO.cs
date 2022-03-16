using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeFit.DAL.Models.DTOs.Workout
{
    public class WorkoutCompleteDTO
    {
        public int WorkoutId { get; set; }
        public bool Complete { get; set; }
    }
}
