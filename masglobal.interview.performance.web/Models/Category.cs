using System;
using System.Collections.Generic;

namespace masglobal.interview.performance.web.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public ICollection<ProductCategory> CategoryProducts { get; } = new List<ProductCategory>();

    }
}