using System;
using System.ComponentModel.DataAnnotations;

namespace DataEntities
{
    public class TransactionDeposit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        public int DepositId { get; set; }
        public Deposit Deposit { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
