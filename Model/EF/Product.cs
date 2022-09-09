using System;
using System.Collections.Generic;

namespace EccommerceV3.Model.EF
{
    public partial class Product
    {
        public Product()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDesc { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
