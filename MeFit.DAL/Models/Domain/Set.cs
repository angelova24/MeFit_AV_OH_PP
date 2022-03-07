namespace MeFitAPI.DAL.Models.Domain
{
    public class Set
    {
        public int Id { get; set; }
        public int ExerciseRepetitions { get; set; }

        //FK with navigation property
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
