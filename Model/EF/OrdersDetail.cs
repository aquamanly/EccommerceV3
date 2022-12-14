using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace EccommerceV3.Model.EF
{
    public partial class OrdersDetail
    {
        public int OrderDetailsId { get; set; }
        public int? ProductId { get; set; }
        public int? ProductQty { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }

        // Created properties and methods
        public decimal? Subtotal { 
            get {
                return ProductQty * ProductPrice;
            }
        }

        //public decimal? OrdersDetail_subtotal (int qty, decimal price) {
        //    return qty * price;
        //}
    }
}
