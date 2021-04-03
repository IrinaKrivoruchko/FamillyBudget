using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataEntities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public int TypeAccountId { get; set; }
        public TypeAccount TypeAccount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<AccountStatement> AccountStatements { get; set; }
    }
}
