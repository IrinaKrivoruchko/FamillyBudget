using AutoMapper;
using DataEntities;
using FamilyDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deposits.Services
{
    public class MapperContainerDeposit : Profile
    {
        public MapperContainerDeposit()
        {
            CreateMap<DepositDto, Deposit>()
                .ForMember(x => x.TransactionDeposits, opt => opt.Ignore())
                .ForMember(y => y.UserId, opt => opt.Ignore())
                .ForMember(z => z.User, opt => opt.Ignore());

            CreateMap<Deposit, DepositDto>();
        }
    }
}
