using AutoMapper;
using DataEntities;
using FamilyDto;

namespace Cashes.Services
{
    public class MapperContainerCash : Profile
    {
        public MapperContainerCash()
        {
            CreateMap<CashDto, Cash>()
                .ForMember(x => x.TransactionCashes, opt => opt.Ignore())
                .ForMember(y => y.UserId, opt => opt.Ignore())
                .ForMember(z => z.User, opt => opt.Ignore()); 
            
            CreateMap<Cash, CashDto>();
        }
    }
}
