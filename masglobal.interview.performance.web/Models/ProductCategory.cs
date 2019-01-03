using System;

namespace masglobal.interview.performance.web.Models
{
    public class ProductCategory
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}