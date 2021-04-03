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
            CreateMap<AccountStatementDto, AccountStatement>();

            CreateMap<AccountStatement, AccountStatementDto>();
        }
    }
}
