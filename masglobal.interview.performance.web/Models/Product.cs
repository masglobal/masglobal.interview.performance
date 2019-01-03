using System;
using System.Collections.Generic;

namespace masglobal.interview.performance.web.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }


        public ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();

    }

}