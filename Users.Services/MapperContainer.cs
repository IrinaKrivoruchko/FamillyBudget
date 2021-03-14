using AutoMapper;
using DataEntities;
using FamilyDto;

namespace Users.Services
{
    public class MapperContainer : Profile
    {
        public MapperContainer()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Wallet, opt => opt.Ignore());

            CreateMap<User, UserDto>();
        }
    }
}
