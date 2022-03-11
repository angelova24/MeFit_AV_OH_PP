using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.Set;

namespace MeFit.API.Profiles
{
    public class SetProfile : AutoMapper.Profile
    {
        public SetProfile()
        {
            CreateMap<Set, SetReadDTO>()
                   .ReverseMap();
        }
    }
}
