using AutoMapper;
using DataEntities;
using FamilyDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionsServices.Accounts.Statements
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
