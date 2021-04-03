using AutoMapper;
using DataEntities;
using FamilyDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Services
{
    public class MapperContainerAccount : Profile
    {
        public MapperContainerAccount()
        {
            CreateMap<AccountDto, Account>();

            CreateMap<Account, AccountDto>();
        }
    }
}
