

using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.Profile;
using System.Linq;

namespace MeFit.API.Profiles
{
    public class ProfilesProfile : AutoMapper.Profile
    {

        public ProfilesProfile()
        {
            CreateMap<Profile, ProfileCreateDTO>()
                .ReverseMap();
            CreateMap<Profile, ProfileReadDTO>()
                .ForMember(pdto => pdto.Goals, opt => opt.MapFrom(p => p.Goals.Select(g => g.Id).ToArray()))
               .ReverseMap();
            CreateMap<Profile, ProfileUpdateDTO>()
               .ReverseMap();
            CreateMap<ProfileReadDTO, ProfileCreateDTO>()
                .ReverseMap();
        }
    }
}
