using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataEntities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
