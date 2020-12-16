using System;

namespace DataEntities
{
    public class TransactionCash
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Balance { get; set; }

        public int CashId { get; set; }
        public Cash Cash { get; set; }
    }
}
