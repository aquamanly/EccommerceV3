using System;
using System.Collections.Generic;

namespace EccommerceV3.Model.EF
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryImg { get; set; }
        public string? CategoryDesc { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
