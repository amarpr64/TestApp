using System;
using System.Collections.Generic;

namespace DAL
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //collection navigation property
        public ICollection<Product> Products { get; set; }
    }
}
