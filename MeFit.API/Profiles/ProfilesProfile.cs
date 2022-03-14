

using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.Profile;

namespace MeFit.API.Profiles
{
    public class ProfilesProfile : AutoMapper.Profile
    {

        public ProfilesProfile()
        {
            CreateMap<Profile, ProfileCreateDTO>()
                .ReverseMap();
            CreateMap<Profile, ProfileReadDTO>()
               .ReverseMap();
            CreateMap<Profile, ProfileUpdateDTO>()
               .ReverseMap();
            CreateMap<Profile, ProfileDeleteDTO>()
                .ReverseMap();
        }
    }
}
