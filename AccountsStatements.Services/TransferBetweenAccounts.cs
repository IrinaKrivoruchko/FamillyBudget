using Common;
using DataEntities;
using DataStorage;
using FamilyDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AccountsStatements.Services
{
    public class TransferBetweenAccounts
    {
        private readonly DatabaseContext _dbContext;
        private readonly IServiceMapper _serviceMapper;

        public TransferBetweenAccounts(DatabaseContext dbContext, IServiceMapper serviceMapper)
        {
            _dbContext = dbContext;
            _serviceMapper = serviceMapper;
        }

        public async Task TransferAccountAsync(int userId, int fromAccountId, int toAccountId, AccountStatementDto accountStatementDto)
        {
            using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
                var fromAccount = _dbContext.Accounts.Include(x => x.AccountStatements)
                    .FirstOrDefault(x => x.Id == fromAccountId);
                var toAccount = _dbContext.Accounts.Include(x => x.AccountStatements)
                    .FirstOrDefault(x => x.Id == toAccountId);

                if (fromAccount == null || toAccount == null || fromAccount == toAccount || fromAccount.UserId == userId || toAccount.UserId == userId)
                {
                    throw new ArgumentException("Bad request");
                }

                fromAccount.Balance -= accountStatementDto.SumTransaction;
                fromAccount.AccountStatements.Add(new AccountStatement
                {
                    DateTime = DateTime.UtcNow,
                    SumTransaction = -1 * accountStatementDto.SumTransaction
                });
                await _dbContext.SaveChangesAsync();

                toAccount.Balance += accountStatementDto.SumTransaction;
                toAccount.AccountStatements.Add(new AccountStatement
                {
                    DateTime = DateTime.UtcNow,
                    SumTransaction = accountStatementDto.SumTransaction
                });

                await _dbContext.SaveChangesAsync();
            }

        }
    }
}

