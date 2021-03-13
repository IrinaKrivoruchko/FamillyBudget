using System;
using System.ComponentModel.DataAnnotations;

namespace DataEntities
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ParentId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
