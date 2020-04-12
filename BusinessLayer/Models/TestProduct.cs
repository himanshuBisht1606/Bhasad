using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class TestProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ProductTypeId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public List<ProductVariant> productVariant { get; set; }
       public List<Item> items { get; set; }
    }
}