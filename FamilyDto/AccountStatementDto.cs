using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FamilyDto
{
    public class AccountStatementDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public decimal SumTransaction { get; set; }

        public int AccountId { get; set; }

        public int CategoryId { get; set; }
    }
}
