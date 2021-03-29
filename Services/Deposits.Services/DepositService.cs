using Common;
using DataEntities;
using DataStorage;
using FamilyDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deposits.Services
{
    public class DepositService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IServiceMapper _serviceMapper;

        public DepositService(DatabaseContext dbContext, IServiceMapper serviceMapper)
        {
            _dbContext = dbContext;
            _serviceMapper = serviceMapper;
        }

        public async Task<DepositDto> CreateDepositForUserAsync(int userId, DepositDto depositDto)
        {
            var user = await _dbContext.Users
                .Include(x => x.Deposits)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException($"Not found user by id {userId}");
            }

            var depositEntityAdd = _serviceMapper.Map<DepositDto, Deposit>(depositDto);
            user.Deposits.Add(depositEntityAdd);
            await _dbContext.SaveChangesAsync();
            return _serviceMapper.Map<Deposit, DepositDto>(depositEntityAdd);
        }

        public async Task<DepositDto> GetDepositAsync(int userId, int depositId)
        {
            var user = await _dbContext.Users
                .Include(x => x.Deposits)
                .FirstOrDefaultAsync(x => x.Id == userId);

            var deposit = user.Deposits.FirstOrDefault(x => x.Id == depositId);

            if (user == null || deposit == null)
            {
                throw new NullReferenceException($"Not found user by id {userId} or depositNumber {depositId} \n Change data");
            }

            return _serviceMapper.Map<Deposit, DepositDto>(deposit);
        }

        public async Task<IEnumerable<DepositDto>> GetAllDepositsForUserAsync(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception($"Not found user by id {userId}");
            }

            return _dbContext.Deposits
                .Where(x => x.UserId == userId)
                .Where(deposit => deposit.UserId == userId)
                .Select(deposit => _serviceMapper.Map<Deposit, DepositDto>(deposit));
        }

        public async Task<DepositDto> PatchDepositAsync(int userId, DepositDto depositDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var deposit = _dbContext.Deposits.FirstOrDefault(x => x.Id == depositDto.Id);

            if(user == null || deposit == null || deposit.UserId != userId)
            {
                throw new Exception($"Not found user by id {userId} or deposit {deposit.Id} is not found \n Change data");
            }

            var depositMerge = _serviceMapper.Merge(depositDto, deposit);
            await _dbContext.SaveChangesAsync();
            var newDepositMergeMap = _serviceMapper.Map<Deposit, DepositDto>(depositMerge);
            return newDepositMergeMap;
        }

        public async Task DeleteDepositAsync(int userId, int depositId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var deposit = _dbContext.Deposits.FirstOrDefault(x => x.Id == depositId);

            if (user == null || deposit == null || deposit.UserId != userId)
            {
                throw new Exception($"Not found user by id {userId} or deposit {deposit.Id} is not found \n Change data");
            }

            _dbContext.Deposits.Remove(deposit);
            await _dbContext.SaveChangesAsync();
        }
    }
}
