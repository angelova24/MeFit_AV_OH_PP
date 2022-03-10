using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.GoalsDTO;

namespace MeFit.API.Profiles
{
    public class GoalsProfile :AutoMapper.Profile
    {
        public GoalsProfile()
        {
            CreateMap<Goal, GoalCreateDTO>()
                .ReverseMap();
            CreateMap<Goal, GoalUpdateDTO>()
                .ReverseMap();
            CreateMap<Goal, GoalReadDTO>()
                .ReverseMap();
            CreateMap<Goal, GoalDeleteDTO>()
                .ReverseMap();
        }
    }
}
