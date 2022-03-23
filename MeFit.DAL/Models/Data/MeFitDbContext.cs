using MeFit.DAL.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MeFit.DAL.Models.Data
{
    public class MeFitDbContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalWorkout> GoalWorkouts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        public MeFitDbContext(DbContextOptions<MeFitDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User seeding
            modelBuilder.Entity<User>()
             .HasData(new User() { Id = 1, Username = "john@example.com", Name = "John Doe", ProfileId = 1 },
                      new User() { Id = 2, Username = "contributor@example.com", Name = "Contributor User", ProfileId = 2 });

            //Profile seeding
            modelBuilder.Entity<Profile>()
            .HasData(new Profile { Id = 1, Disabilities = "none", Height = 1.70, MedicalConditions = "healthy", Weight = 78.05, FintessLevelEvaluation          = 2 },
                     new Profile { Id = 2, Disabilities = "none", Height = 1.70, MedicalConditions = "healthy", Weight = 78.05, FintessLevelEvaluation = 5 });

            //Exercise seeding
            modelBuilder.Entity<Exercise>()
                .HasData(new Exercise { Id = 1, Name = "Burpees", Description = "Conditioning exercise in which a person squats, places the palms of the hands on the floor in front of the feet, jumps back into a push-up position", TargetMuscleGroup = "Whole body", VideoURL = "https://www.thephysedexpress.com/uploads/3/1/1/1/31119283/burpee_orig.gif" },
                  new Exercise { Id = 2, Name = "Calf raises", Description = "Raise your heels slowly, keeping your knees extended. Pause for one second when you're standing as much on the tips of your toes as you can. Lower your heels back to the ground, returning to the starting position.", TargetMuscleGroup = "Legs", VideoURL = "https://www.thephysedexpress.com/uploads/3/1/1/1/31119283/calf-raises_orig.gif" },
                  new Exercise { Id = 3, Name = "Jump squats", Description = "Jump squats are bodyweight exercises characterized by leaping directly upwards at the top of the movement", TargetMuscleGroup = "Legs", VideoURL = "https://www.thephysedexpress.com/uploads/3/1/1/1/31119283/jump-squats_orig.gif" },
                  new Exercise { Id = 4, Name = "Jumping lunges", Description = "The jump lunge is an advanced variation of a basic walking lunge exercise, bumping up the intensity by adding a jump.", TargetMuscleGroup = "Legs", VideoURL = "https://www.thephysedexpress.com/uploads/3/1/1/1/31119283/jumping-lunges_orig.gif" },
                  new Exercise { Id = 5, Name = "Leg raises", Description = "Lay flat with your arms at your sides and legs stretched out next to each other, then raise those legs.", TargetMuscleGroup = "Abs", VideoURL = "https://www.thephysedexpress.com/uploads/3/1/1/1/31119283/leg-raises_orig.gif" });

            //Set seeding
            modelBuilder.Entity<Set>()
                .HasData(new Set { Id = 1, ExerciseId = 1, ExerciseRepetitions = 10, OwnerId = 2 },
                         new Set { Id = 2, ExerciseId = 2, ExerciseRepetitions = 10, OwnerId = 2 },
                         new Set { Id = 3, ExerciseId = 3, ExerciseRepetitions = 10, OwnerId = 2 },
                         new Set { Id = 4, ExerciseId = 4, ExerciseRepetitions = 10, OwnerId = 2 },
                         new Set { Id = 5, ExerciseId = 5, ExerciseRepetitions = 10, OwnerId = 2 },
                         new Set { Id = 6, ExerciseId = 1, ExerciseRepetitions = 15, OwnerId = 2 },
                         new Set { Id = 7, ExerciseId = 2, ExerciseRepetitions = 15, OwnerId = 2 },
                         new Set { Id = 8, ExerciseId = 3, ExerciseRepetitions = 15, OwnerId = 2 },
                         new Set { Id = 9, ExerciseId = 4, ExerciseRepetitions = 15, OwnerId = 2 },
                         new Set { Id = 10, ExerciseId = 5, ExerciseRepetitions = 15, OwnerId = 2 });

            //Workout seeding
            modelBuilder.Entity<Workout>()
                .HasData(new Workout { Id = 1, Name = "Basic", Type = "Beginner", OwnerId = 2 },
                         new Workout { Id = 2, Name = "Basic+", Type = "Beginner", OwnerId = 2 },
                         new Workout { Id = 3, Name = "Jump", Type = "Competent", OwnerId = 2 });

            //SetWorkout seeding
            modelBuilder.Entity("SetWorkout").HasData(
                         new { SetsId = 2, WorkoutsId = 1 },
                         new { SetsId = 5, WorkoutsId = 1 },
                         new { SetsId = 1, WorkoutsId = 2 },
                         new { SetsId = 2, WorkoutsId = 2 },
                         new { SetsId = 5, WorkoutsId = 2 },
                         new { SetsId = 8, WorkoutsId = 3 },
                         new { SetsId = 9, WorkoutsId = 3 });

            //Program seeding
            modelBuilder.Entity<Program>().HasData(
                         new Program { Id = 1, Name = "Get in summer shape", Category = "loose fat", OwnerId = 2 },
                         new Program { Id = 2, Name = "Covid - do not leave home", Category = "no gym", OwnerId = 2 },
                         new Program { Id = 3, Name = "Conquer one's weaker self", Category = "beginner", OwnerId = 2 }
                         );

            //ProgramWorkout seeding
            modelBuilder.Entity("ProgramWorkout").HasData(
                         new { ProgramsId = 1, WorkoutsId = 1 },
                         new { ProgramsId = 1, WorkoutsId = 2 },
                         new { ProgramsId = 2, WorkoutsId = 3 });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
