using System.Collections.Generic;

namespace DataEntities
{
    public class Wallet
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Balance { get; set; }

        public ICollection<Card> Cards { get; set; }
        public ICollection<Cash> Cashes { get; set; }
        public ICollection<Deposit> Deposits { get; set; }
    }
}
