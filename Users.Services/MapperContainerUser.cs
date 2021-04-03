using AutoMapper;
using DataEntities;
using FamilyDto;

namespace Users.Services
{
    public class MapperContainerUser : Profile
    {
        public MapperContainerUser()
        {
            CreateMap<UserDto, User>()
                .ForMember(x => x.Accounts, y => y.Ignore());

            CreateMap<User, UserDto>();
        }
    }
}
