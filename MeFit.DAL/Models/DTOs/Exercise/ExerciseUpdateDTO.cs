using System.ComponentModel.DataAnnotations;

namespace MeFit.DAL.Models.DTOs.Exercise
{
    public class ExerciseUpdateDTO
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
        public int OwnerId { get; set; }
    }
}
