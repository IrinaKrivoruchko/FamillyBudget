using Common;
using DataEntities;
using DataStorage;
using FamilyDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AccountsStatements.Services
{
    public class AccountStatementService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IServiceMapper _serviceMapper;

        public AccountStatementService(DatabaseContext dbContext, IServiceMapper serviceMapper)
        {
            _dbContext = dbContext;
            _serviceMapper = serviceMapper;
        }

        public async Task AccountOperationAsync([FromRoute] int userId, [FromRoute] int accountId, [FromBody] AccountStatementDto accountStatementDto)
        {
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
                var account = _dbContext.Accounts.Include(x => x.AccountStatements)
                    .FirstOrDefault(x => x.Id == accountId);

                if (account == null || account.UserId == userId)
                {
                    throw new ArgumentException("Bad request");
                }

                account.Balance += accountStatementDto.SumTransaction;
                account.AccountStatements.Add(new AccountStatement
                {
                    DateTime = DateTime.UtcNow,
                    SumTransaction = accountStatementDto.SumTransaction
                });

                await _dbContext.SaveChangesAsync();
            }
        }

    }

}
