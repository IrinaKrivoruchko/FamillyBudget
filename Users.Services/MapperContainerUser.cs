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
                .ForMember(x => x.Cards, opt => opt.Ignore())
                .ForMember(y => y.Cashes, opt => opt.Ignore())
                .ForMember(z => z.Deposits, opt => opt.Ignore());

            CreateMap<User, UserDto>();
        }
    }
}
