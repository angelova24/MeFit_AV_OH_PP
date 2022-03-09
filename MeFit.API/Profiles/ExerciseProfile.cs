using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.Exercise;

namespace MeFit.API.Profiles
{
    public class ExerciseProfile : AutoMapper.Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExerciseCreateDTO>()
                .ReverseMap();
            CreateMap<Exercise, ExerciseReadDTO>()
                .ReverseMap();
        }
    }
}
