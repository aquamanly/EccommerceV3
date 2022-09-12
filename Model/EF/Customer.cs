using System;
using System.Collections.Generic;

namespace EccommerceV3.Model.EF
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? LoginId { get; set; }
        public string? Address { get; set; }
        public int? Postal_Code { get; set; }
        public string? City { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
