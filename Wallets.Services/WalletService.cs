using DataStorage;
using FamilyDto;
using System;
using System.Linq;

namespace Wallets.Services
{
    public class WalletService
    {
        private readonly DatabaseContext _dbContext;

        public WalletService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WalletDto CreateWallet(int userId, WalletDto walletDto)
        {
            //TODO
            //1. userid does not have a wallet
            //2. WalletDto - Wallet
            //3. Save Wallet to Database
            //4. Return new WalletDto
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user), $"Not found user by id {userId}");
            }
            return walletDto;
        }
    }
}
