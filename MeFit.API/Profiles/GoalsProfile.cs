using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.Goal;
using System.Linq;

namespace MeFit.API.Profiles
{
    public class GoalsProfile :AutoMapper.Profile
    {
        public GoalsProfile()
        {
            CreateMap<Goal, GoalCreateDTO>()
                .ReverseMap();
            CreateMap<Goal, GoalReadDTO>().ForMember(gdto => gdto.Workouts, opt =>
             opt.MapFrom(g => g.Workouts.Select(g => g.Id).ToArray()))
                .ReverseMap();
            CreateMap<Goal, GoalWorkoutsDTO>()
                .ReverseMap();
            CreateMap<GoalWorkout, GoalWorkoutReadDTO>()
                .ReverseMap();
            CreateMap<GoalWorkout, GoalWorkoutCreateDTO>()
                .ReverseMap();
        }
    }
}
