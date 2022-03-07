namespace MeFitAPI.DAL.Models.Domain
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TargetMuscleGroup { get; set; }
        public string Image { get; set; }
        public string VideoURL { get; set; }
    }
}
