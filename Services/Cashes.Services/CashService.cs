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

namespace Cashes.Services
{
    public class CashService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IServiceMapper _serviceMapper;

        public CashService(DatabaseContext dbContext, IServiceMapper serviceMapper)
        {
            _dbContext = dbContext;
            _serviceMapper = serviceMapper;
        }

        public async Task<CashDto> CreateCashesForUserAsync(int userId, CashDto cashDto)
        {
            var user = await _dbContext.Users
                .Include(x => x.Cashes)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if(user == null)
            {
                throw new NullReferenceException($"Not found user by id {userId}");
            }

            var cashEntityAdd = _serviceMapper.Map<CashDto, Cash>(cashDto);
            user.Cashes.Add(cashEntityAdd);
            await _dbContext.SaveChangesAsync();
            return _serviceMapper.Map<Cash, CashDto>(cashEntityAdd);
        }

        public async Task<CashDto> GetCashAsync(int userId, int cashId)
        {
            var user = await _dbContext.Users
                .Include(x => x.Cashes)
                .FirstOrDefaultAsync(x => x.Id == userId);
            var cash = user.Cashes.FirstOrDefault(x => x.Id == cashId);

            if(user == null || cash == null)
            {
                throw new Exception($"Not found user by id {userId} or cashNumer {cashId} \n Change data");
            }

            var card = user.Cashes.FirstOrDefault(x => x.Id == cashId);
            return _serviceMapper.Map<Cash, CashDto>(cash);
        }

        public async Task<IEnumerable<CashDto>> GetAllCashesForUser(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception($"Not found user by id {userId}");
            }

            return _dbContext.Cashes
                .Where(x => x.UserId == userId)
                .Where(cash => cash.UserId == userId)
                .Select(cash => _serviceMapper.Map<Cash, CashDto>(cash));
        }

        public async Task<Cash> PatchCashAsync(int userId, CashDto cashDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var cash = _dbContext.Cashes.FirstOrDefault(x => x.Id == cashDto.Id);

            if(user == null || cash == null || cash.UserId != userId)
            {
                throw new Exception($"Not found user by id {userId} or card {cash.Id} is not found \n Change data");
            }

            var cashMerge = _serviceMapper.Merge(cashDto, cash);
            await _dbContext.SaveChangesAsync();
            return cashMerge;
        }

        public async Task DeleteCashAsync(int userId, int cashId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var cash = _dbContext.Cashes.FirstOrDefault(x => x.Id == cashId);

            if(user == null || cash == null || cash.UserId == userId)
            {
                throw new Exception($"Not found user by id {userId} or card {cash.Id} is not found \n Change data");
            }

            _dbContext.Cashes.Remove(cash);
            await _dbContext.SaveChangesAsync();
        }
    }
}
