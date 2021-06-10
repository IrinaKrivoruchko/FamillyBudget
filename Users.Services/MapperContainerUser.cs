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
                .ForMember(x => x.Accounts, y => y.Ignore())
                .ForMember(x => x.Role, y => y.Ignore())
                .ForMember(x => x.RoleId, y => y.Ignore());

            CreateMap<User, UserDto>();
        }
    }
}
