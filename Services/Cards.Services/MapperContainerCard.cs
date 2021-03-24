using AutoMapper;
using DataEntities;
using FamilyDto;

namespace Cards.Services
{
    public class MapperContainerCard : Profile
    {
        public MapperContainerCard()
        {
            CreateMap<CardDto, Card>()
                .ForMember(x => x.TransactionCards, opt => opt.Ignore())
                .ForMember(y => y.UserId, opt => opt.Ignore())
                .ForMember(z => z.User, opt => opt.Ignore());

            CreateMap<Card, CardDto>();
        }
    }
}
