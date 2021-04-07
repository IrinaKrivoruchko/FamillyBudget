using AutoMapper;
using DataEntities;
using FamilyDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsStatements.Services
{
    public class MapperContainerAccountStatement : Profile
    {
        public MapperContainerAccountStatement()
        {
            CreateMap<AccountStatementDto, AccountStatement>()
                .ForAllMembers(x => x.Ignore());

            CreateMap<AccountStatement, AccountStatementDto>();
        }
    }
}
