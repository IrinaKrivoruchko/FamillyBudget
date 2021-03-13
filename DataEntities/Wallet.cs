using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataEntities
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Card> Cards { get; set; }
        public ICollection<Cash> Cashes { get; set; }
        public ICollection<Deposit> Deposits { get; set; }
    }
}
