using AutoMapper;
using DataEntities;
using FamilyDto;
using System;

namespace Accounts.Services
{
    class TypeConverter : IValueConverter<string, TypeAccount>
    {
        public TypeAccount Convert(string sourceMember, ResolutionContext context)
        {
            return (TypeAccount)Enum.Parse(typeof(TypeAccount), sourceMember, true);
        }
    }

    class TypeConverterToString : IValueConverter<TypeAccount, string>
    {
        public string Convert(TypeAccount sourceMember, ResolutionContext context)
        {
            return sourceMember.ToString("G");
        }
    }

    public class MapperContainerAccount : Profile
    {
        public MapperContainerAccount()
        {
            CreateMap<AccountDto, Account>()
                .ForMember(dest => dest.Type, src => src.ConvertUsing(new TypeConverter(), x => x.Type))
                .ForMember(dest => dest.UserId, src => src.Ignore())
                .ForMember(dest => dest.User, src => src.Ignore())
                .ForMember(dest => dest.AccountStatements, src => src.Ignore());

            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.Type, src => src.ConvertUsing(new TypeConverterToString(), x => x.Type));
        }
    }
}
