using System.Collections.Generic;

namespace DataEntities
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }

        public ICollection<TransactionCard> TransactionCards { get; set; }
    }
}
