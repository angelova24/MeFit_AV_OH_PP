using MeFit.DAL.Models.Domain;
using MeFit.DAL.Models.DTOs.UserDTOs;
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
            CreateMap<User, UserUpdateDTO>()
               .ReverseMap();
            CreateMap<User, UserDeleteDTO>()
                .ReverseMap();
        }
    }
}
