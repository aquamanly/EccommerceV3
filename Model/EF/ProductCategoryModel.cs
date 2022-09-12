using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EccommerceV3.Model.EF
{
    public class ProductCategoryViewModel
    {
        public List<Product>? Products { get; set; }
        public SelectList? Categories { get; set; }
        public int? ProductCategories { get; set; }
        public string? SearchString { get; set; }
    }
}