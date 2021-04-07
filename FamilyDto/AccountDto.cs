using System.ComponentModel.DataAnnotations;

namespace FamilyDto
{
    public class AccountDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        [AccountType]
        public string Type { get; set; }
    }
}
