using System;
using System.ComponentModel.DataAnnotations;

namespace DataEntities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime DayOfBirthday { get; set; }

        public Wallet Wallet { get; set; }
    }
}
