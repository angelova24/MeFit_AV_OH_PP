using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.Program;
using System.Linq;

namespace MeFit.API.Profiles
{
    public class ProgramProfile : AutoMapper.Profile
    {
        public ProgramProfile()
        {
            CreateMap<Program, ProgramReadDTO>()
               .ForMember(pdto => pdto.Workouts, opt =>
             opt.MapFrom(p => p.Workouts.Select(w => w.Id).ToArray()))
               .ReverseMap();
        }
    }
}
