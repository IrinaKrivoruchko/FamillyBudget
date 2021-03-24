using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataEntities
{
    public class Deposit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal Balance { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<TransactionDeposit> TransactionDeposits { get; set; }
    }
}
