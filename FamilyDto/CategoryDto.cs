using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyDto
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }
    }
}
