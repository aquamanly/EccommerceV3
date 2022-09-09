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
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LoginId { get; set; }
        public string? Address { get; set; }
        public int? PostCode { get; set; }
        public string? City { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
