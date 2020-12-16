using System;

namespace DataEntities
{
    public class TransactionCard
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Balance { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
