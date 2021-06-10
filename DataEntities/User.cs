using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataEntities
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
