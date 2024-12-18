using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coffee_api.Dtos.Category
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}