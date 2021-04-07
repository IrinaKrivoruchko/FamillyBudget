using Common;
using DataStorage;
using DataEntities;
using FamilyDto;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Services
{
    public class AccountService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IServiceMapper _serviceMapper;

        public AccountService(DatabaseContext dbContext, IServiceMapper serviceMapper)
        {
            _dbContext = dbContext;
            _serviceMapper = serviceMapper;
        }

        public async Task<IQueryable<AccountDto>> GetAllAccountsForUser(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NullReferenceException($"Not found user by id {userId}");
            }

            return _dbContext.Accounts
                 .Where(x => x.UserId == userId)
                 .Where(account => account.UserId == userId)
                 .Select(account => _serviceMapper.Map<Account, AccountDto>(account));
        }

        public async Task<AccountDto> GetAccountAsync(int userId, int accountId)
        {
            var user = await _dbContext.Users
                .Include(x => x.Accounts)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NullReferenceException($"Not found user by id {userId}");
            }

            var account = user.Accounts.FirstOrDefault(x => x.Id == accountId);
            return _serviceMapper.Map<Account, AccountDto>(account);
        }

        public async Task<AccountDto> CreateAccountsForUserAsync(int userId, AccountDto accountDto)
        {
            var user = await _dbContext.Users
                .Include(x => x.Accounts)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException($"This user {userId} does not exist");
            }

            var accountEntityAdd = _serviceMapper.Map<AccountDto, Account>(accountDto);
            user.Accounts.Add(accountEntityAdd);
            await _dbContext.SaveChangesAsync();
            accountDto.Id = accountEntityAdd.Id;
            return _serviceMapper.Map<Account, AccountDto>(accountEntityAdd);
        }

        public async Task DeleteAccountAsync(int userId, int accountId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var account = _dbContext.Accounts.FirstOrDefault(x => x.Id == accountId);
            if (user == null || account == null || account.UserId != userId)
            {
                throw new Exception($"Not found user by id {userId} or card {account.Id} is not found \n Change data");
            }

            _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<AccountDto> PatchAccountAsync(int userId, AccountDto accountDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var account = _dbContext.Accounts.FirstOrDefault(x => x.Id == accountDto.Id);

            if (user == null || account == null || account.UserId != userId)
            {
                throw new Exception($"Not found user by id {userId} or card {account.Id} is not found \n Change data");
            }

            var accountMerge = _serviceMapper.Merge(accountDto, account);
            await _dbContext.SaveChangesAsync();
            var newAccountMergeMap = _serviceMapper.Map<Account, AccountDto>(accountMerge);
            return newAccountMergeMap;
        }
    }
}
