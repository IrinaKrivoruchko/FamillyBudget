using System;

namespace DataEntities
{
    public class TransactionDeposit
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Balance { get; set; }

        public int DepositId { get; set; }
        public Deposit Deposit { get; set; }
    }
}
