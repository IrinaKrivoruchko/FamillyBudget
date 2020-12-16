using System.Collections.Generic;

namespace DataEntities
{
    public class Wallet
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Balance { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Card> Cards { get; set; }
        public ICollection<Cash> Cashes { get; set; }
        public ICollection<Deposit> Deposits { get; set; }
    }
}
