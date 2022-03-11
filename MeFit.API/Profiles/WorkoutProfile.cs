using AutoMapper;
using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.Set;
using MeFit.DAL.Models.DTOs.Workout;
using System.Linq;

namespace MeFit.API.Profiles
{
    public class WorkoutProfile : AutoMapper.Profile
    {
        public WorkoutProfile()
        {
            CreateMap<Workout, WorkoutReadDTO>()
                .ForMember(wdto => wdto.Sets,  opt =>
               opt.MapFrom(w => w.Sets.Select(s => s.Id).ToArray()))
                .ForMember(wdto => wdto.Programs, opt =>
              opt.MapFrom(w => w.Programs.Select(p => p.Id).ToArray()))
                .ForMember(wdto => wdto.Goals, opt =>
              opt.MapFrom(w => w.Goals.Select(g => g.Id).ToArray()))
               .ReverseMap();

            CreateMap<Workout, WorkoutCreatDTO>()
                .ReverseMap();

            CreateMap<Workout, WorkoutSetDTO>()
                .ForMember(wdto => wdto.Sets, opt =>
                opt.MapFrom(w => w.Sets))
                .ReverseMap();
        }
    }
}
