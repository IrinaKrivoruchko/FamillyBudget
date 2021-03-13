using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataEntities
{
    public class Cash
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal Balance { get; set; }

        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }

        public ICollection<TransactionCash> TransactionCashes { get; set; }
    }
}
