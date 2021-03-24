using System;
using System.ComponentModel.DataAnnotations;

namespace DataEntities
{
    public class TransactionCard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
