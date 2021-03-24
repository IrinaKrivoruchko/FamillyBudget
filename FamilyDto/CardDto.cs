using System.ComponentModel.DataAnnotations;

namespace FamilyDto
{
    public class CardDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [CreditCard]
        public string NumberCard { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}
