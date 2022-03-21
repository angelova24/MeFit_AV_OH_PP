using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.User;
using System.Linq;



namespace MeFit.API.Profiles
{
    public  class UsersProfile : AutoMapper.Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserCreateDTO>()
                .ReverseMap();
            CreateMap<User, UserReadDTO>()                
               .ReverseMap();
            CreateMap<UserCreateDTO, UserReadDTO>()
              .ReverseMap();

        }
    }
}
